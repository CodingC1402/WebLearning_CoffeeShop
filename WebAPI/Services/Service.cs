using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Data.Models;
using WebAPI.Utilities.Extensions;

namespace WebAPI.Services;

// Provide basic method for CRUD operations other than that the derived class will provide the rest.
public abstract class Service {}