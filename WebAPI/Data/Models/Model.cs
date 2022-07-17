using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Models;

public abstract class Model
{
    public int Id { get; init; }
}