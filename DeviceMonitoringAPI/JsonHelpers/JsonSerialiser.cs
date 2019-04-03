using Newtonsoft.Json;

namespace JsonHelpers
{
    public class JsonSerialiser<T> : IJsonSerialiser<T> where T : class
    {
        public string Serialise(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
