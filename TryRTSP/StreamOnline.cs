using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryRTSP
{
    
    class StreamOnline
    {
        private static string streamTwitchKey = "live_91267640_ZiqIu8swsrOhN0RZ224CNLJEkA5Gub";
        private static string streamTwitchPrefix = "rtmp://sin.contribute.live-video.net/app/";

        private static string streamVimeowPrefix = "rtmps://rtmp-global.cloud.vimeo.com:443/live/";
        private static string streamVimeowKey = "592e6d5a-799b-4094-b4a5-512afa1e058b";

        public static string buildTwitchLink() {
            return streamTwitchPrefix + streamTwitchKey;
        }

        public static string buildVimeowLink() {
            return streamVimeowPrefix + streamVimeowKey;
        }
    }
}
