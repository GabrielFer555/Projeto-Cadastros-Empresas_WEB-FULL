using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.UpdateModelViews.Classes
{
    public class FornecedorUpdateModelView
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string? Rg { get; set; }
        public DateOnly? DataNasc { get; set; }
        public string Uf { get; set; }
        public string TipoFornecedor { get; set; }
        public int EmpresaVinculada { get; set; }
        public string Telefone_1 { get; set; }
        public string? Telefone_2 { get; set; }
        public string? Celular { get; set; }

        public FornecedorUpdateModelView(string name, string document, int empresaVinculada, string uf, string tipoFornecedor, string? rg, DateOnly? dataNasc, string telefone_1, string? telefone_2, string? celular)
        {
            // Adicione validações de dados aqui, se necessário
            this.Name = name;
            this.Document = document;
            this.Uf = uf;
            this.TipoFornecedor = tipoFornecedor;
            this.Rg = rg;
            this.DataNasc = dataNasc;
            this.EmpresaVinculada = empresaVinculada;
            this.Telefone_1 = telefone_1;
            this.Telefone_2 = telefone_2;
            this.Celular = celular;
        }
    }
}