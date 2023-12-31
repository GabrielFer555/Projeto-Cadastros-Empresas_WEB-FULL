using backend.Classes;
using backend.Data;
using backend.ModelViews.Classes;
using backend.UpdateModelViews.Classes;
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
        [HttpPost] 
        [Route("Producer/Create")]
        public async Task<IActionResult> Post([FromServices] AppDbContext dbContext, [FromBody] ModelFornecedorHttp fornecedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(fornecedor.Name == "" || fornecedor.Name == null){
                return BadRequest("Nome tem que ser informado!");
            }
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
            if (fornecedor.TipoFornecedor == "f")
            {
                if(fornecedor.Document.Length < 14){
                    return BadRequest("CPF Inválido");
                }
                
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
        [HttpDelete]
        [Route("Producer/Remove/{id}")]
        public async Task<IActionResult> deleteProducer([FromServices] AppDbContext dbContext, [FromRoute] int id ){
            var Fornecedor = await dbContext.fornecedors_cad.FirstOrDefaultAsync(x => x.Id == id);
            if(Fornecedor == null){
                return NotFound();
            }
            try{
                dbContext.Remove(Fornecedor);
                await dbContext.SaveChangesAsync();
                return Ok();
            }catch(Exception){
                return BadRequest("Erro ao deletar usuário!");
            }
        }

        [HttpPut] 
        [Route("Producer/Update/{id}")]
        public async Task<IActionResult> PutProducer([FromServices] AppDbContext dbContext, [FromBody] FornecedorUpdateModelView fornecedor, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(fornecedor.EmpresaVinculada == 0){
                return BadRequest("EmpresaVinculada não pode ser zero!");
            }
            var Fornecedor = await dbContext.fornecedors_cad.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            var Emp = await dbContext.empresa_cad.FirstOrDefaultAsync(x => x.EmpresaId == fornecedor.EmpresaVinculada);
             if(Fornecedor == null){
                return NotFound("Código de Fornecedor não encontraodo!");
             }
            if (Emp == null)
            {
                return NotFound("Empresa não existe");
            }
            if(fornecedor.Name == ""){
                return BadRequest("Nome tem que ser informado!");
            }
            if(fornecedor.TipoFornecedor != "f" && fornecedor.TipoFornecedor !="j"){
                return BadRequest("Tipo fornecedor precisa ser especificado.");
            }
            if(fornecedor.EmpresaVinculada == 0){
                return BadRequest("campo 'EmpresaVinculada' é invalida");
            }
            if (fornecedor.TipoFornecedor == "f")
            {
                if(fornecedor.Document.Length < 14){
                    return BadRequest("CPF Inválido");
                }
                
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
            }
            if (fornecedor.TipoFornecedor == "j" && fornecedor.Document.Length < 18)
            {
                    return BadRequest("CNPJ invalido");
            }
            
            Fornecedor.Name = fornecedor.Name;
            Fornecedor.Document = fornecedor.Document;
            Fornecedor.Rg = fornecedor.Rg;
            Fornecedor.DataNasc = fornecedor.DataNasc;
            Fornecedor.TipoFornecedor = fornecedor.TipoFornecedor;
            Fornecedor.Uf = fornecedor.Uf;
            Fornecedor.EmpresaVinculada = fornecedor.EmpresaVinculada;
            Fornecedor.Telefone_1 = fornecedor.Telefone_1;
            Fornecedor.Telefone_2 = fornecedor.Telefone_2;
            Fornecedor.Celular = fornecedor.Celular;
            try{
                dbContext.Update(Fornecedor);
                await dbContext.SaveChangesAsync();
                return Ok(Fornecedor);
            }catch(Exception){
                return BadRequest("Impossível Salvar");
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