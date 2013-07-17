using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoApp.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace DemoApp.Data.Maps
{
    class BookMap: EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            Property(t => t.Copyright).HasMaxLength(200);
            
            // Table & Column Mappings
            ToTable("Book");
            Property(t => t.Copyright).HasColumnName("Copyright");
            Property(t => t.Published).HasColumnName("Published");
            Property(t => t.AuthorId).HasColumnName("AuthorId");

			HasRequired(t => t.Author).WithMany().HasForeignKey(t => t.AuthorId);
        }
    }
}