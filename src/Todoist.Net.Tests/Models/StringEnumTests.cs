using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Tests.Models
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class StringEnumTests
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new StringEnumTypeConverter()
            }
        };

        [Fact]
        public void TryParse_InvalidValue_Fail()
        {
            Assert.False(StringEnum.TryParse("all1", out ResourceType result));
            Assert.Null(result);
        }

        [Fact]
        public void TryParse_ValidValue_Success()
        {
            Assert.True(StringEnum.TryParse("all", out ResourceType result));
            Assert.NotNull(result);
        }

        [Fact]
        public void Serialize_DictionaryWithStringEnumKeys_UsesStringEnumValues()
        {
            var dictionary = new Dictionary<ResourceType, string>
            {
                [ResourceType.All] = "value"
            };

            var json = JsonSerializer.Serialize(dictionary, SerializerOptions);

            Assert.Equal("{\"all\":\"value\"}", json);
        }

        [Fact]
        public void Deserialize_DictionaryWithStringEnumKeys_IsCaseInsensitive()
        {
            var dictionary = JsonSerializer.Deserialize<Dictionary<ResourceType, string>>("{\"ALL\":\"value\"}", SerializerOptions);

            Assert.NotNull(dictionary);
            var entry = Assert.Single(dictionary);
            Assert.Equal(ResourceType.All, entry.Key);
            Assert.Equal("value", entry.Value);
        }
    }
}
