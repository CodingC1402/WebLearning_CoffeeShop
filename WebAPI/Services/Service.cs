using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Models;
using WebAPI.Data.Repos;

namespace WebAPI.Services;

public abstract class Service {}
public abstract class Service<R, M> : Service where R : Repository<M> where M : Model
{
    public R Repository { get; private set; }
    public Service(R repository) {
        this.Repository = repository;
    }
}