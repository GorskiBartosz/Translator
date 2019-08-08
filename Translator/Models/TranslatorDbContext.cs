using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Translator.Models
{
    /// <summary>
    /// DB context for DataBase interactions from EntityFramework
    /// </summary>
    public class TranslatorDbContext : DbContext
    {
        public DbSet<Translation> Translations{ get; set; }
    }
}