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
        public static void RemoveRange<T>(this DbSet<T> @this, Expression<Func<T, bool>> expression) where T : class {
            @this.RemoveRange(@this.Where(expression));
        }

        public static void RemoveAll<T>(this DbSet<T> @this) where T : class {
            @this.RemoveRange(@this);
        }

        public static IQueryable<T> FindById<T> (this DbSet<T> @this, int id) where T : Model {
            return @this.Where(x => x.Id == id);
        }

        public static void RemoveById<T>(this DbSet<T> @this, int id) where T : Model {
            @this.RemoveRange(@this.FindById(id));
        }

        public static async Task<T> GetRandomEntity<T>(this DbSet<T> @this) where T : class {
            var rnd = Random.Shared.Next(0, await @this.CountAsync());
            return await @this.Skip(rnd).Take(1).SingleAsync();
        }
    }
}