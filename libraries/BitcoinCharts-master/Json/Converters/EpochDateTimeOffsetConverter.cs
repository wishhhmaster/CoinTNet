using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinCharts.Json.Converters {
    internal class EpochDateTimeOffsetConverter : JsonConverter {
        private static DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        public EpochDateTimeOffsetConverter() {
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(double);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Integer) {
                var value = (long)reader.Value;
                return Epoch.AddSeconds(value);
            }
            if(reader.TokenType == JsonToken.String) {
                var value = long.Parse((string)reader.Value);
                return Epoch.AddMilliseconds(value / 1000);
            }
            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}