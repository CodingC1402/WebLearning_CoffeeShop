using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Data.Repos;

public class OrderRepo : Repository<Order>
{
    public OrderRepo(DbContext dbContext) : base(dbContext)
    {}

    public async Task<decimal> ComputeTotal(Order entity) {
        var total = await Context
            .Entry(entity)
            .Collection(e => e.Details!)
            .Query()
            .Include(detail => detail.Coffee)
            .SumAsync(detail => detail.Coffee!.Price * detail.Count);
        
        typeof(Order).GetProperty(nameof(entity.Total))!.SetValue(entity, total);
        return total;
    }
    public async Task ComputeTotal(params Order[] entities) {
        Task[] tasks = new Task[entities.Length];
        for (int i = 0; i < entities.Length; i++) {
            Order entity = entities[i];
            tasks[i] = ComputeTotal(entity);
        }

        await Task.WhenAll(tasks);
    }
}