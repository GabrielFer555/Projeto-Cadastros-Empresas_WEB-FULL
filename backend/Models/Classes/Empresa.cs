using backend.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.Models.Classes
{
    public class Empresa : IEnterprise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpresaId { get; set; }
        public string Name { get ; set; }
        public string Document { get; set; }
        public string Uf { get; set; }

        public Empresa(string name, string document, string Uf){
            this.Name = name;
            this.Document = document;
            this.Uf = Uf;
        }
    }
}