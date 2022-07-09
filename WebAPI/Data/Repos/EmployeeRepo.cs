using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;

namespace WebAPI.Data.Repos;

public class EmployeeRepo : Repository<Employee>
{
    public EmployeeRepo(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Employee> GetRandomEmployee() {
        int count = await DataSet.CountAsync();
        int index = Random.Shared.Next(0, count);

        return await DataSet.Skip(index).Take(1).SingleAsync();
    }

    public async Task RemoveAllManagedEmployee(int managerId) {
        await DataSet.Where(e => e.ManagerId == managerId).ForEachAsync(e => e.ManagerId = null);
        await Save();
    }

    public async Task AddManageEmployee(int managerId, params int[] employeeId) {
        await DataSet.Where(e => employeeId.Contains(e.Id)).ForEachAsync(e => e.ManagerId = managerId);
    }
}