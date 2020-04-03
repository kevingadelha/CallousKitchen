﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Capstone
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class CallousHippoEntities : DbContext
    {
        public CallousHippoEntities()
            : base("name=CallousHippoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<DietTag> DietTags { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    
        public virtual ObjectResult<AccountLogin_Result> AccountLogin(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AccountLogin_Result>("AccountLogin", emailParameter, passwordParameter);
        }
    
        public virtual ObjectResult<CreateAccount_Result> CreateAccount(string username, string email, string password, Nullable<int> dietId, Nullable<int> guiltLevel)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var dietIdParameter = dietId.HasValue ?
                new ObjectParameter("DietId", dietId) :
                new ObjectParameter("DietId", typeof(int));
    
            var guiltLevelParameter = guiltLevel.HasValue ?
                new ObjectParameter("GuiltLevel", guiltLevel) :
                new ObjectParameter("GuiltLevel", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CreateAccount_Result>("CreateAccount", usernameParameter, emailParameter, passwordParameter, dietIdParameter, guiltLevelParameter);
        }
    }
}
