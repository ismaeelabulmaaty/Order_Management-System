using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order_Manag.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oredr_Manag.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {


            builder.Property(o => o.Status)
               .HasConversion(
               ostatus => ostatus.ToString(),
               ostatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), ostatus));


            builder.Property(O => O.TotalAmount)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
