using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Utilities.Converters
{
    public class GenderPictJsonConverter : JsonConverter<string>
    {
        private static readonly Regex s_regex = new(@"[^0-9]+");
        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Match match = s_regex.Match(reader.GetString() ?? "");
            return match.Value.CapitalizeFirstLetter();
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            => writer.WriteStringValue(value);
    }
}