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
        private readonly IConfiguration _configuration;
        
        public AppDbContext(IConfiguration configuration){
            _configuration = configuration;
        }
        public DbSet<Empresa> empresa_cad { get; set; }/*Representa a tabela de empresa*/
        public DbSet<Fornecedor> fornecedors_cad { get; set;}/*Representa a tabela de fornecedores*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured){
                String connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
            
        }
    }
}