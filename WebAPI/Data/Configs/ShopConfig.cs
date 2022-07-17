using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Models;

namespace WebAPI.Data.Configs;

public class ShopConfig : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.Property(x => x.Address).HasMaxLength(100);
        builder.Property(x => x.Phone).HasMaxLength(22);
        builder.Property(x => x.EstablishedSince).HasColumnType("Date");
    }
}