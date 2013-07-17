using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using DemoApp.Data.Entities;

namespace DemoApp.Data.Maps
{
    public class ContentObjectMap : EntityTypeConfiguration<ContentObject>
    {
        public ContentObjectMap()
        {
			HasKey(t => t.Id);

			// Properties
			// Table & Column Mappings
            ToTable("ContentObject");
			Property(t => t.Id).HasColumnName("Id");
			Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
		}
    }
}