using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
  public class Content
  {
    private class Resource
    {
      public string path;
      public object obj;

      public void Dispose()
      {
        if (obj is System.Drawing.Bitmap)
          ((System.Drawing.Bitmap)obj).Dispose();

        if (obj is Audio)
          ((Audio)obj).Dispose();
      }
    }

    private List<Resource> resources;
    public string ResourcePath = @"\res\";

    public Content()
    {
      resources = new List<Resource>();
    }

    /// <summary>
    /// Loads content in memory the first time, if a file is accessed a second time, its re-used from memory and not loaded twice
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="file">File path relative to Content.ResourcePath</param>
    /// <returns></returns>
    public T Load<T>(string file)
    {
      if (resources.Where(x => x.path == Environment.CurrentDirectory + ResourcePath + "\\" + file).ToList().Count() == 0)
      {
        Log.Write("Loading [" + System.IO.Path.GetFileName(Environment.CurrentDirectory + ResourcePath + "\\" + file) + "]");

        
        object instance = (object)Activator.CreateInstance(typeof(T), new object[] { Environment.CurrentDirectory + ResourcePath + "\\" + file });

        Resource res = new Resource();
        res.path = Environment.CurrentDirectory + ResourcePath + "\\" + file;
        res.obj = (T)Convert.ChangeType(instance, typeof(T));
        resources.Add(res);

        Log.Done();
        return (T)Convert.ChangeType(instance, typeof(T));
      }
      else
      {
        return (T)Convert.ChangeType(resources.Where(x => x.path == Environment.CurrentDirectory + ResourcePath + "\\" + file).ToList()[0].obj, typeof(T));
      }
    }

    public void Dispose()
    {
        foreach (var res in resources)
        {
            res.path = "";
            res.Dispose();
            res.obj = null;
        }
    }

  }
}
