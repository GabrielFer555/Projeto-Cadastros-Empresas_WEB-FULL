using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Classes;
using backend.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class AppDbContext: DbContext /*Faz o De-para das classses para o Banco*/
    {
        public DbSet<Empresa> empresa_cad { get; set; }/*Representa a tabela de empresa*/
        public DbSet<Fornecedor> fornecedors_cad { get; set;}/*Representa a tabela de fornecedores*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(@"Server=dpg-cl9v105o7jlc73fjttp0-a.oregon-postgres.render.com;Port=5432;Database=mydb_hqtj;User Id=gabriel;Password=7bPmAKYo9BQguU6TPlGn3etazLlW6WCj;");
        
    }
}