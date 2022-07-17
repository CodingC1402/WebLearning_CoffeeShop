using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Models;

namespace WebAPI.Data.Configs
{
    public class CoffeeConfig : IEntityTypeConfiguration<Coffee>
    {
        public void Configure(EntityTypeBuilder<Coffee> builder)
        {
            builder.HasIndex(coffee => coffee.Name).IsUnique(true);
            builder.Property(x => x.Name).HasMaxLength(30).IsUnicode();
            builder.Property(x => x.Notes).HasMaxLength(128);
            builder.Property(x => x.Origin).HasMaxLength(128).IsUnicode();
            builder.Property(x => x.Variety).HasMaxLength(128).IsUnicode();
            builder.Property(x => x.Price).HasPrecision(4, 2);
        }
    }
}