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
        try{
            var producers =  await dbContext.fornecedors_cad.AsNoTracking().ToListAsync();
            return Ok(producers);
        }catch(Exception e){
            return BadRequest(e);
        }
            
       }

       [HttpGet]
       [Route("Producer/{id}")]
       public async Task<IActionResult> GetbyId([FromServices] AppDbContext dbContext, [FromRoute] int id){
            var Fornecedor = dbContext.fornecedors_cad.FirstOrDefaultAsync(x => x.Id == id);
            if(Fornecedor == null){
                return NotFound();
            }else{
                return Ok(Fornecedor);
            }
       }


    }
}