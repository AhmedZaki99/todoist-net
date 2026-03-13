using System.Text.Json;
using System.Text.Json.Serialization;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Tests.Models
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class MoveSectionArgumentTests
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new ComplexIdConverter()
            }
        };

        [Fact]
        public void Constructor_WithoutProjectId_LeavesProjectIdNull()
        {
            var argument = new MoveSectionArgument(new ComplexId("section-id"));

            Assert.Null(argument.ProjectId);
        }

        [Fact]
        public void Serialize_WithoutProjectId_IncludesNullProjectId()
        {
            var argument = new MoveSectionArgument(new ComplexId("section-id"));

            var json = JsonSerializer.Serialize(argument, SerializerOptions);

            Assert.Contains("\"project_id\":null", json);
        }
    }
}
