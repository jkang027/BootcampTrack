using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampTrack.Core.Utility
{
    public class Security
    {
        public static string GetTimeStampedToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = new Guid().ToByteArray();
            return Convert.ToBase64String(time.Concat(key).ToArray());
        }
    }
}
