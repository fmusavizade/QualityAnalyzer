using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Utilities
{
    public static class JsonExtention
    {
        public static string ToJsonString<T>(this T model) where T : class => Encoding.UTF8.GetString(model.ToJson<T>());

        public static byte[] ToJson<T>(this T model) where T : class
        {
            if (model == null) return null;
            using (var memoryStream = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(T)).WriteObject(memoryStream, model);
                return memoryStream.ToArray();
            }
        }

        public static T JsonStringToObject<T>(this string jsonString) where T : class => Encoding.UTF8.GetBytes(jsonString).JsonBufferToObject<T>();
        public static T JsonBufferToObject<T>(this byte[] jsonBuffer) where T : class
        {
            using (MemoryStream memoryStream = new MemoryStream(jsonBuffer))
                return new DataContractJsonSerializer(typeof(T)).ReadObject((Stream)memoryStream) as T;
        }
    }
}
