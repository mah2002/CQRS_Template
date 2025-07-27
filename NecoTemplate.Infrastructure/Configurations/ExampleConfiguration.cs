using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using NecoTemplate.Domain.Models.Examples;

namespace NecoTemplate.Infrastructure.Configurations;

internal sealed class ExampleConfiguration : IEntityTypeConfiguration<Example>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Example> builder)
    {
        builder.ToTable("example");

        builder.HasKey(example => example.Id);

        builder.Property<uint>("Version").IsRowVersion();
    }
}
