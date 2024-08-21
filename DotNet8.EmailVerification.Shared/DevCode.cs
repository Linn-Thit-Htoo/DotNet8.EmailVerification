using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Shared
{
    public static class DevCode
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

        public static T ToObject<T>(this string jsonStr) => JsonConvert.DeserializeObject<T>(jsonStr)!;
    }
}
