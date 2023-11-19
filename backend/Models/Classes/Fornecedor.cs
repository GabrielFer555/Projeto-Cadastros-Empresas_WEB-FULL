using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Interfaces;
using backend.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace backend.Classes
{
    public class Fornecedor : IEnterprise
    {   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime DataCad { get; set; }
        public String? Rg { get; set; }
        public DateOnly? DataNasc { get; set;}
        public String Uf { get; set; }
        public String TipoFornecedor { get; set;}
        public int EmpresaVinculada{ get; set;}
         public String Telefone_1 { get; set; }
         public String? Telefone_2{ get; set; }
        public String? Celular { get; set; }





    public Fornecedor( string name, string document,int empresaVinculada, String uf,  String tipoFornecedor, DateTime dataCad, String? rg, DateOnly? dataNasc, String telefone_1, String telefone_2, String celular){
        this.Name = name;
        this.Document = document;
        this.Uf = uf;
        this.TipoFornecedor = tipoFornecedor;
        this.DataCad = dataCad;
        this.Rg = rg;
        this.DataNasc = dataNasc;
        this.EmpresaVinculada = empresaVinculada;
        this.Telefone_1 = telefone_1;
        this.Telefone_2 = telefone_2;
        this.Celular = celular;
    }

    }
}