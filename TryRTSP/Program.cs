using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace TryRTSP
{
    class Program
    {
  
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Task task = convertVideoImageCameraOnvif();
            Task task = streamToTwitch(); 
            task.Wait();

        }


        public static string buildName(int number)
        {
           string path =  "G://VSCsharp/tmp_out/fileNameNo" + number + ".png";
           Console.WriteLine(path);
           return path;
        }

  

        /// <summary>
        /// Read RTPS stream from camera and output image
        /// </summary>
        /// <returns></returns>
        public static async Task convertVideoImageCameraOnvif()
        {
            FFmpeg.SetExecutablesPath("C://ffmpeg/bin/");
            var mediaInfo = await FFmpeg.GetMediaInfo("rtsp://admin:Aa123456@192.168.1.2:554/onvif1");
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            cancellationTokenSource.CancelAfter(10000);
            System.Func<string, string> outputFileNameBuilder = (number) => { return "G://VSCsharp/tmp_out/onvif/fileNameNo" + number + ".png"; };
            IConversionResult conversionResult = await FFmpeg.Conversions.New()
                .AddStream(mediaInfo.Streams.First())
                .ExtractEveryNthFrame(10, outputFileNameBuilder)
                .Start(cancellationTokenSource.Token);
        }


        public static async Task streamToVimeo()
        {
            FFmpeg.SetExecutablesPath("C://ffmpeg/bin/");
            var mediaInfo = await FFmpeg.GetMediaInfo("rtsp://admin:Aa123456@192.168.1.2:554/onvif1");
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            cancellationTokenSource.CancelAfter(10000);
            IConversionResult conversionResult = await FFmpeg.Conversions.New()
                .AddStream(mediaInfo.Streams.First())
                .SetOutput(StreamOnline.buildVimeowLink())
                .Start();
        }

        public static async Task streamToTwitch()
        {
            FFmpeg.SetExecutablesPath("C://ffmpeg/bin/");
            var mediaInfo = await FFmpeg.GetMediaInfo("rtsp://admin:Aa123456@192.168.1.2:554/onvif1");
            var cancellationTokenSource = new System.Threading.CancellationTokenSource();
            cancellationTokenSource.CancelAfter(10000);

            IStream videoStream = mediaInfo.VideoStreams.FirstOrDefault().SetCodec(VideoCodec.libx264).SetBitrate(3000000);
            IStream audioStream = mediaInfo.AudioStreams.FirstOrDefault().SetCodec(AudioCodec.aac).SetBitrate(192000);

            IConversionResult conversionResult = await FFmpeg.Conversions.New()
                .AddStream(videoStream, audioStream)
                .SetOutput(StreamOnline.buildTwitchLink())
                .SetOutputFormat(Format.flv)
                .Start();
        }

    }
}
