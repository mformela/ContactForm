using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactForm.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("ContactForm")
        {

        }

        public DbSet<ContactForm> ContactForms { get; set; }

    }
}