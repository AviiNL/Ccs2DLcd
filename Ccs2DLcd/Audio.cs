using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Runtime.InteropServices;
using NAudio;
using NAudio.Wave;
using System.Threading;

namespace Ccs2DLcd
{
  public class Audio
  {
    IWavePlayer waveOutDevice;
    WaveChannel32 mainOutputStream;
    public bool Repeat = false;

    public Audio(string wavfile)
    {
      waveOutDevice = new WaveOut(WaveCallbackInfo.FunctionCallback());

      mainOutputStream = new WaveChannel32(new WaveFileReader(wavfile));
      mainOutputStream.Seek(0, System.IO.SeekOrigin.Begin);
      waveOutDevice.Init(mainOutputStream);

    }

    /// <summary>
    /// Set the volume of audio object
    /// </summary>
    /// <param name="Volume">Volume between 1 and 100</param>
    public void SetVolume(int Volume)
    {
      if (mainOutputStream != null)
        mainOutputStream.Volume = (float)Volume / 100;
      
    }

    public void Play()
    {
      if (mainOutputStream != null)
        mainOutputStream.Seek(0, System.IO.SeekOrigin.Begin);
      
      waveOutDevice.Stop();
      waveOutDevice.Play();

    }

    public void Stop()
    {
      waveOutDevice.Stop();
    }

    public void Update()
    {
      if (mainOutputStream.Length <= mainOutputStream.Position)
        if (Repeat)
        {
          Play();
        }
        else
        {
          Stop();
        }
    }

    public void Dispose()
    {
      mainOutputStream.Dispose();
      waveOutDevice.Dispose();
    }

  }
}
