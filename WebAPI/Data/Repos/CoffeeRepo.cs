using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Data.Repos;

public class CoffeeRepo : Repository<Coffee>
{
    public CoffeeRepo(DbContext dbContext) : base(dbContext)
    {}

        public async Task<Coffee> GetRandomCoffee() {
        int count = await DataSet.CountAsync();
        int index = Random.Shared.Next(0, count);

        return await DataSet.Skip(index).Take(1).SingleAsync();
    }
}