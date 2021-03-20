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
            Task task = convertVideoImageCameraOnvif();
            task.Wait();

        }


        public static string buildName(int number)
        {
           string path =  "G://VSCsharp/tmp_out/fileNameNo" + number + ".png";
           Console.WriteLine(path);
           return path;
        }

  

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

    }
}
