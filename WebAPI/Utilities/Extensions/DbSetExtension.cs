using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Utilities.Extensions
{
    public static class DbSetExtension
    {
        public static async Task RemoveRange<T>(this DbSet<T> @this, Expression<Func<T, bool>> expression) where T : class {
            @this.RemoveRange(await @this.Where(expression).ToArrayAsync());
        }

        public static async Task RemoveAll<T>(this DbSet<T> @this) where T : class {
            @this.RemoveRange(await @this.ToArrayAsync());
        }

        public static async Task<T> FindById<T>(this DbSet<T> @this, int id) where T : Model {
            return await @this.Where(x => x.Id == id).SingleAsync();
        }

        public static async Task<T> GetRandomEntity<T>(this DbSet<T> @this) where T : class {
            var rnd = Random.Shared.Next(0, await @this.CountAsync());
            return await @this.Skip(rnd).Take(1).SingleAsync();
        }
    }
}