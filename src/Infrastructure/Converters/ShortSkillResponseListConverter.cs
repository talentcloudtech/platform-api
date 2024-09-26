using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Platform.Domain.Entities.Common;

namespace Platform.Infrastructure.Converters;

/// <summary>
/// Converts an array of <see cref="ShortSkillResponse"/> to a JSON string and vice versa.
/// </summary>
public class ShortSkillResponseListConverter : ValueConverter<List<ShortSkillResponse>, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShortSkillResponseListConverter"/> class.
    /// </summary>
    public ShortSkillResponseListConverter()
        : base(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
            v =>
                JsonSerializer.Deserialize<List<ShortSkillResponse>>(v, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })
                ?? new List<ShortSkillResponse>()
        ) { }
}
