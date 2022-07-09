using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Converters;

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private static readonly string s_format = "yyyy-MM-dd";

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        var result = DateTime.ParseExact(reader.GetString()!, s_format, CultureInfo.InvariantCulture);
        return result;
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime dateOnlyValue,
        JsonSerializerOptions options) 
        => writer.WriteStringValue(dateOnlyValue.ToString(s_format, CultureInfo.InvariantCulture));
}