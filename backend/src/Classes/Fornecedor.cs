using backend.Interfaces;

namespace backend.Classes
{
    public class Fornecedor : IEnterprise
    {
        public string name { get; set; }
        public string document { get ; set ; }
        
        

        public Fornecedor(string name, string document){
            this.name = name;
            this.document = document;
        }
    }
}