﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotepadServiceRole
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NotepadSharedEntities : DbContext
    {
        public NotepadSharedEntities()
            : base("name=NotepadSharedEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupNote> GroupNote { get; set; }
        public DbSet<GroupUser> GroupUser { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<User> User { get; set; }
    }
}
