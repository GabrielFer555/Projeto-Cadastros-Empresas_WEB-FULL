using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.ModelViews.Classes
{
    public class ModelEmpresaHttp
    {
        public string Name { get ; set; }
        public string Document { get; set; }
        public string Uf { get; set; }
        
        public ModelEmpresaHttp (string name, string document, string Uf){
            this.Name = name;
            this.Document = document;
            this.Uf = Uf;
        }
    }
}