using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
public class ApiControllerBase<S> : ControllerBase where S : Service
{
    public S Service { get; init; }

    public ApiControllerBase(S service)
    {
        this.Service = service;
    }
}