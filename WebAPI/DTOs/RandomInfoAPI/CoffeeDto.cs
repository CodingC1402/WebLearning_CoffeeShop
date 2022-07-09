using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.DTOs.RandomInfoAPI;

public record CoffeeDto (
    [property:JsonPropertyName("blend_name")] string Name,
    [property:JsonPropertyName("origin")] string Origin,
    [property:JsonPropertyName("variety")] string Variety,
    [property:JsonPropertyName("notes")] string Notes
    )
{}