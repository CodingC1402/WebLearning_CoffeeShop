using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;

namespace WebAPI.Services;

public class CoffeeService : Service<CoffeeRepo, Coffee>
{
    public CoffeeService(CoffeeRepo repository) : base(repository)
    {
    }
}