using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using backend.Data;
using backend.Models.Classes;
using backend.ModelViews.Classes;
using backend.UpdateModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;

namespace backend.Controllers
{
    [ApiController]
    [Route("v1")]
    public class EmpresaController : ControllerBase
    {
        [HttpGet]
        [Route("Company")]
        public async Task<IActionResult> Gets([FromServices] AppDbContext dbContext)
        {
            try
            {
                var empresas = await dbContext.empresa_cad.AsNoTracking().ToListAsync();
                return Ok(empresas);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetCompanyById/{id}")]
        public async Task<IActionResult> GetById([FromServices] AppDbContext dbContext, [FromRoute] int id)
        {
            try
            {
                var singleCompany = await dbContext.empresa_cad.AsNoTracking().FirstOrDefaultAsync(x => x.EmpresaId == id);
                return singleCompany != null ? Ok(singleCompany) : NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("Company")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext dbContext, [FromBody] ModelEmpresaHttp empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var Empresa = new Empresa(empresa.Name, empresa.Document, empresa.Uf);
            try
            {
                await dbContext.empresa_cad.AddAsync(Empresa);
                await dbContext.SaveChangesAsync();
                return Created($"api/v1/Company/{Empresa.EmpresaId}", Empresa);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("UpdateCompany/{EmpresaId}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext dbContext, [FromRoute] int EmpresaId, [FromBody] EmpresaUpdateModelView empresaUpdate)
        {
            var empresa = await dbContext.empresa_cad.FirstOrDefaultAsync(x => x.EmpresaId == EmpresaId);
            if (empresa == null)
            {
                return NotFound();
            }
            empresa.Name = empresaUpdate.Name;
            empresa.Uf = empresaUpdate.Uf;
            try
            {

                dbContext.Update(empresa);
                await dbContext.SaveChangesAsync();
                return Ok(empresa);

            }
            catch (Exception)
            {
                return BadRequest("Erro Interno!");
            }

        }
        [HttpDelete]
        [Route("DeleteCompany/{id}")]
    public async Task<IActionResult> Delete([FromServices] AppDbContext dbContext, [FromRoute] int id ){
        var empresaVinculada = await dbContext.fornecedors_cad.AsNoTracking().FirstOrDefaultAsync(x => x.EmpresaVinculada == id);
        if(empresaVinculada != null){
            return StatusCode(422, "Empresa vinculada com algum fornecedor, impossível excluir");
        }
        var empresaADeletar = await dbContext.empresa_cad.FirstOrDefaultAsync(x => x.EmpresaId == id);
        if(empresaADeletar == null){
            return NotFound();
        }
        try{
            dbContext.Remove(empresaADeletar);
            await dbContext.SaveChangesAsync();
            return Ok("Removido com Sucesso");
        }catch (Exception){
            return BadRequest("Erro Interno!");
        }
    }
}
}
