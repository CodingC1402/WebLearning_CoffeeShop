using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI.Utilities.Attributes;
using WebAPI.Utilities.Converters;

namespace WebAPI.DTOs.RandomInfoAPI;

public record PeopleDto(
    [property:JsonPropertyName("name")] string FullName,
    [property:JsonPropertyName("email_u")] string Email,
    [property:JsonPropertyName("phone_w")] string Phone,
    [property:JsonPropertyName("birth_data"), JsonConverter(typeof(DateTimeJsonConverter))] DateTime DOB,
    [property:JsonPropertyName("pict"), JsonConverter(typeof(GenderPictJsonConverter)), Gender] string Gender,
    [property:JsonPropertyName("password")] string Password
    )
{}