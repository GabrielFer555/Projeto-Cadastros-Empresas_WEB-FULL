using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FornecedorController : Controller
    {
       
       [HttpGet]
       [Route("Producer")]
       public async Task<IActionResult> GetOverall([FromServices] AppDbContext dbContext){
            var producers =  await dbContext.fornecedors_cad.AsNoTracking().ToListAsync();
            return Ok(producers);
       }

       
    }
}