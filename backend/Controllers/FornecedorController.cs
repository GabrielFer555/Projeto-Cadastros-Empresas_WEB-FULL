using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using backend.Classes;
using backend.Data;
using backend.ModelViews.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    [Route("v1")]
    public class FornecedorController : Controller
    {

        [HttpGet]
        [Route("Producer/FindAll")]
        public async Task<IActionResult> GetOverall([FromServices] AppDbContext dbContext)
        {
            try
            {
                var producers = await dbContext.fornecedors_cad.AsNoTracking().ToListAsync();
                return Ok(producers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet]
        [Route("Producer/Find/{id}")]
        public async Task<IActionResult> GetbyId([FromServices] AppDbContext dbContext, [FromRoute] int id)
        {
            var fornecedor = await dbContext.fornecedors_cad.FirstOrDefaultAsync(x => x.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fornecedor);
            }
        }
        [HttpPost]  //!Under Development
        [Route("Producer/Create")]
        public async Task<IActionResult> Post([FromServices] AppDbContext dbContext, [FromBody] Fornecedor fornecedor)
        {
            if(fornecedor.EmpresaVinculada == 0){
                return BadRequest("campo 'EmpresaVinculada' é invalida");
            }
            try{
            var Emp = await dbContext.empresa_cad.FirstOrDefaultAsync(x => x.EmpresaId == fornecedor.EmpresaVinculada);
            if (Emp == null)
            {
                return BadRequest("Empresa não existe");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (fornecedor.TipoFornecedor == "f")
            {
                String Uf = Emp.Uf;
                DateOnly dataNasc = (DateOnly)fornecedor.DataNasc;
                if (Uf == "PR" && calcularIdade(dataNasc) < 18)
                {
                    return BadRequest("Pessoa fisica menor de idade não pode ser Vinculado a empresa do Paraná");
                }
                else
                {
                    try{
                        await dbContext.AddAsync(fornecedor);
                        await dbContext.SaveChangesAsync();
                        return Created($"v1/Producer/Find/{fornecedor.Id}",fornecedor);
                    }catch(Exception e){
                        return BadRequest("Erro Interno!");
                    }
                }
            }
            else
            {
                if(fornecedor.Document.Length < 19){
                    return BadRequest("CNPJ invalido");
                }
                else{
                    try{
                        await dbContext.AddAsync(fornecedor);
                        await dbContext.SaveChangesAsync();
                        return Created($"v1/Producer/Find/{fornecedor.Id}",fornecedor);
                    }   catch(Exception e){
                        return BadRequest(e.GetBaseException());
                    }
                }
            }
            }catch(Exception e){
                return BadRequest("Erro Interno!");
            }
            
        }
        [NonAction]
        public int calcularIdade(DateOnly data)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);
            int diff = dataAtual.Year - data.Year;
            if (data.DayOfYear < dataAtual.DayOfYear)
            {
                diff--;
            }

            return diff;
        }



    }
}