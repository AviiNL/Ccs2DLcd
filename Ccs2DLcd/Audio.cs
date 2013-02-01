using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Runtime.InteropServices;
using NAudio;
using NAudio.Wave;
using NAudio.Ogg;

using System.Threading;

namespace Ccs2DLcd
{
    public class Audio
    {
        IWavePlayer waveOutDevice;
        WaveStream mainOutputStream;
        public bool Repeat = false;
        public bool IsPlaying { get; private set; }
        string fileName;
        int Volume;
        public Audio(string fileName)
        {
            this.fileName = fileName;
            if (!System.IO.File.Exists(fileName)) throw new System.IO.FileNotFoundException();
            try
            {
                waveOutDevice = new DirectSoundOut(50);
            }
            catch (Exception driverCreateException)
            {
                Console.WriteLine(String.Format("{0}", driverCreateException.Message));
                return;
            }
            

            mainOutputStream = CreateInputStream(fileName);
            try
            {
                waveOutDevice.Init(mainOutputStream);
            }
            catch (Exception initException)
            {
                Console.WriteLine(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }
            waveOutDevice.PlaybackStopped += waveOutDevice_PlaybackStopped;
            IsPlaying = false;
        }

        void waveOutDevice_PlaybackStopped(object sender, EventArgs e)
        {
            IsPlaying = false;
            if (Repeat)
            {
                Play();
            }
        }

        /// <summary>
        /// Set the volume of audio object
        /// </summary>
        /// <param name="Volume">Volume between 1 and 100</param>
        public void SetVolume(int Volume)
        {
            this.Volume = Volume;

            if (mainOutputStream != null)
                (mainOutputStream as WaveChannel32).Volume = (float)Volume / 100;

        }

        public void Play()
        {
            waveOutDevice.Stop();
            mainOutputStream.Dispose();
            mainOutputStream = CreateInputStream(fileName);
            waveOutDevice.Init(mainOutputStream);
            (mainOutputStream as WaveChannel32).Volume = (float)Volume / 100;
            waveOutDevice.Play();
            IsPlaying = true;
        }

        public void Stop()
        {

            waveOutDevice.Stop();
            mainOutputStream.Position = mainOutputStream.Seek(0, System.IO.SeekOrigin.Begin);
            IsPlaying = false;
        }


        public void Update()
        {
            if (mainOutputStream.CurrentTime >= mainOutputStream.TotalTime)
            {
                Stop();
                IsPlaying = false;
            }
        }

        public void Dispose()
        {
            mainOutputStream.Dispose();
            waveOutDevice.Dispose();
        }



        private static WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".wav"))
            {
                WaveStream readerStream = new WaveFileReader(fileName);
                if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm)
                {
                    readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                    readerStream = new BlockAlignReductionStream(readerStream);
                }
                if (readerStream.WaveFormat.BitsPerSample != 16)
                {
                    var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                       16, readerStream.WaveFormat.Channels);
                    readerStream = new WaveFormatConversionStream(format, readerStream);
                }
                inputStream = new WaveChannel32(readerStream);
            }
            else if (fileName.EndsWith(".ogg"))
            {

                WaveStream readerStream = new OggFileReader(fileName);
                if (readerStream.WaveFormat.BitsPerSample != 16)
                {
                    var format = new WaveFormat(readerStream.WaveFormat.SampleRate,
                       16, readerStream.WaveFormat.Channels);
                    readerStream = new WaveFormatConversionStream(format, readerStream);
                }
                inputStream = new WaveChannel32(readerStream);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }
            return inputStream;
        }

    }
}
