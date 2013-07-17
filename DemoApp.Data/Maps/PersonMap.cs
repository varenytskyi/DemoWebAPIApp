using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DemoApp.Data.Entities;

namespace DemoApp.Data.Maps
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            Property(t => t.FirstName).HasMaxLength(50);
            Property(t => t.LastName).HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Person");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.Gender).HasColumnName("Gender");
            Property(t => t.BirthDate).HasColumnName("BirthDate");
        }
    }
}