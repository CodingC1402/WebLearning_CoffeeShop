using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;

namespace WebAPI.Services;

public abstract class Service {}
public abstract class Service<R, M> : Service where R : Repository<M> where M : Model
{
    protected R Repository { get; private set; }
    public async Task<IDbContextTransaction> CreateTransaction() => await Repository.StartTransaction();
    public Service(R repository) {
        this.Repository = repository;
    }
}