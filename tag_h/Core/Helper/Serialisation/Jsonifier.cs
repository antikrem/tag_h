using System;
using System.IO;
using Newtonsoft.Json;

using tag_h.Injection;

namespace tag_h.Core.Helper.Extensions
{
    public class InvalidTypeForProvidedJson : Exception
    {
        public string Json { get; }
        public JsonReaderException Inner { get; }

        public InvalidTypeForProvidedJson(Type type, string json, JsonReaderException inner) 
            : base($"Invalid json for {type.Name}" )
        {
            Json = json;
            Inner = inner;
        }

    }

    [Injectable]
    public interface IJsonifier
    {
        public string Jsonify<T>(T o);

        public T ParseJson<T>(string json);
    }

    public class Jsonifier : IJsonifier
    {
        private readonly JsonSerializer _serializer;

        public Jsonifier()
        {
            _serializer = new JsonSerializer();
        }

        public string Jsonify<T>(T o)
        {
            using StringWriter sw = new();
            using JsonWriter writter = new JsonTextWriter(sw);

            _serializer.Serialize(writter, o);

            return sw.ToString();
        }

        public T ParseJson<T>(string json)
        {
            using StringReader sr = new(json);
            var type = typeof(T);

            try
            {
                return (T)_serializer.Deserialize(sr, type);
            }
            catch (JsonReaderException e)
            {
                throw new InvalidTypeForProvidedJson(type, json, e);
            }
        }
    }
}
