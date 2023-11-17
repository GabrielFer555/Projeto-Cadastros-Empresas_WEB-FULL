using backend.Classes;
using backend.Data;
using backend.ModelViews.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            catch (Exception)
            {
                return BadRequest("Erro ao conectar com o Banco!");
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
        public async Task<IActionResult> Post([FromServices] AppDbContext dbContext, [FromBody] ModelFornecedorHttp fornecedor)
        {
            if(fornecedor.TipoFornecedor != "f" && fornecedor.TipoFornecedor !="j"){
                return BadRequest("Tipo fornecedor precisa ser especificado.");
            }
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
                
                if(fornecedor.DataNasc == null){
                    return StatusCode(422,"Data de Nascimento não pode ser vazia");
                }
                String Uf = Emp.Uf;
                DateOnly dataNasc = (DateOnly)fornecedor.DataNasc;
                if(dataNasc.Year < 1900 || dataNasc.Month > 12 || dataNasc > DateOnly.FromDateTime(DateTime.Now)){
                    return StatusCode(422, "Data de Nascimento Invalída");
                }
                if (Uf == "PR" && calcularIdade(dataNasc) < 18)
                {
                    return StatusCode(406, "Pessoa fisica menor de idade não pode ser Vinculado a empresa do Paraná");
                }
                else
                {
                    var Fornecedor = new Fornecedor(fornecedor.Name, fornecedor.Document, fornecedor.EmpresaVinculada, fornecedor.Uf, fornecedor.TipoFornecedor, fornecedor.DataCad, fornecedor.Rg, fornecedor.DataNasc, fornecedor.Telefone_1, fornecedor?.Telefone_2, fornecedor?.Celular);
                    try{
                        await dbContext.AddAsync(Fornecedor);
                        await dbContext.SaveChangesAsync();
                        return Created($"v1/Producer/Find/{Fornecedor.Id}",Fornecedor);
                    }catch(Exception){
                        return BadRequest("Erro Interno!");
                    }
                }
            }
            else
            {
                if(fornecedor.Document.Length < 18){
                    return BadRequest("CNPJ invalido");
                }
                else{
                    try{
                        var Fornecedor = new Fornecedor(fornecedor.Name, fornecedor.Document, fornecedor.EmpresaVinculada, fornecedor.Uf, fornecedor.TipoFornecedor, fornecedor.DataCad, fornecedor.Rg, fornecedor.DataNasc, fornecedor.Telefone_1, fornecedor?.Telefone_2, fornecedor?.Celular);
                        await dbContext.AddAsync(Fornecedor);
                        await dbContext.SaveChangesAsync();
                        return Created($"v1/Producer/Find/{Fornecedor.Id}",fornecedor);
                    }   catch(Exception){
                        return BadRequest("Erro ao Salvar Usuário!");
                    }
                }
            }
            }catch(Exception){
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