using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace TryRTSP
{
    class Example
    {
        public async Task meowAsync() {
            string output = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".mp4");
            var mediaInfo = await FFmpeg.GetMediaInfo("rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov");

            var conversionResult = await FFmpeg.Conversions.New()
                .AddStream(mediaInfo.Streams)
                .SetOutput(output)
                .Start();
        }
    }
}
