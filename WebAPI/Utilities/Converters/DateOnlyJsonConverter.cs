using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Utilities.Converters;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private static readonly string s_format = "yyyy-MM-dd";

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        var result = DateOnly.ParseExact(reader.GetString()!, s_format, CultureInfo.InvariantCulture);
        return result;
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateOnly dateOnlyValue,
        JsonSerializerOptions options) 
        => writer.WriteStringValue(dateOnlyValue.ToString(s_format, CultureInfo.InvariantCulture));
}