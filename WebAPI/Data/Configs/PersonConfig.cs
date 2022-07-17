using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Data.Models;

namespace WebAPI.Data.Configs
{
    public class PersonConfig<T> : IEntityTypeConfiguration<T> where T : Person
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.DOB).HasColumnType("Date");
            builder.Property(x => x.FullName).HasMaxLength(128);
        }
    }
}