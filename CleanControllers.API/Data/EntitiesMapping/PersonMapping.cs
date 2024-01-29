using CleanControllers.API.Constants;
using CleanControllers.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanControllers.API.Data.EntitiesMapping;

public sealed class PersonMapping : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons", PersonSchemaConstants.PersonSchema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Gender)
            .IsRequired(true)
            .HasColumnName("gender");

        builder.Property(p => p.Email)
            .IsRequired(true)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");

        builder.Property(p => p.Phone)
            .IsRequired(true)
            .HasColumnName("phone")
            .HasColumnType("varchar(11)");
    }
}
