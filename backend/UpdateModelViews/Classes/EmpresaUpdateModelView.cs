using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.UpdateModelViews
{
    public class EmpresaUpdateModelView
    {
        public String Name { get; set; }
        public String Uf { get; set; }

        public EmpresaUpdateModelView(String name, String uf){
            this.Name = name;
            this.Uf = uf;
        }
    }
}