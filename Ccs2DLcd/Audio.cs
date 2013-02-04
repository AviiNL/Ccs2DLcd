using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.Ogg;
using System.Threading;

namespace Ccs2DLcd
{
    public class Audio
    {
        public Boolean Repeat { get; set; }
        public Boolean isPlaying { get; internal set; }
        public float Volume
        {
            get
            {
                return ((mainOutputStream as WaveChannel32).Volume * 100);
            }
            set
            {
                (mainOutputStream as WaveChannel32).Volume = value / 100;
            }
        }

        IWavePlayer waveOutDevice;
        WaveStream mainOutputStream;
        String fileName;

        public Audio(string filename)
        {
            this.fileName = filename;
            waveOutDevice = new DirectSoundOut(50);
            mainOutputStream = CreateInputStream(filename);
            waveOutDevice.Init(mainOutputStream);
            isPlaying = false;
        }

        public void Play()
        {
            Reset();
            if (!isPlaying)
            {
                isPlaying = true;
                waveOutDevice.Play();
                new Thread(new ThreadStart(counter)).Start();
            }
        }

        public void Pause()
        {
            if (isPlaying)
            {
                isPlaying = false;
                waveOutDevice.Stop();
            }
        }

        public void Reset()
        {
            mainOutputStream.Position = 0;

        }

        private void counter()
        {
            while (isPlaying)
            {
                Thread.Sleep(50);
                if (mainOutputStream.Position >= mainOutputStream.Length)
                {
                    isPlaying = false;
                }
            }
            if (Repeat)
                Play();

        }

        public void Dispose()
        {
            if (mainOutputStream != null)
                mainOutputStream.Dispose();

            if (waveOutDevice != null)
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
