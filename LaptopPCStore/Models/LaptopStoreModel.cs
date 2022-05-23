using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopPCStore.Models
{
    //add-migration -Context LaptopStoreContext
    //update-database -Context LaptopStoreContext
   
        public class Laptop                     //USE PROP to generate 
        {
            [Key]
            public int lap_id { get; set; }
            public string lap_name { get; set; }
            public string lap_cat { get; set; }
            public int lap_ram { get; set; }
            public string lap_cpu { get; set; }
            public string lap_gpu { get; set; }
            public string lap_disp_size { get; set; }
            public string lap_disp_res { get; set; }
            public string lap_disp_rr { get; set; }
            public string lap_battrey { get; set; }
            public string lap_storage { get; set; }
            public int lap_quantity { get; set; }
        }

        public class Vendor
        {
            [Key]
            public int ven_id { get; set; }
            public string ven_name { get; set; }
            public string ven_number { get; set; }
            public string ven_mail { get; set; }
            public string ven_address { get; set; }      

        }

        public class Purchase
        {
            [Key]
            public int purchase_id { get; set; }

            [ForeignKey("fk2")]
            public int? ven_id { get; set; }
            public Vendor fk2 { get; set; }

            [ForeignKey("fk3")]
            public int? lap_id { get; set; }
            public Laptop fk3 { get; set; }

            public int purchase_quantity { get; set; }
            public DateTime purchase_date { get; set; }
            public int purchase_cost { get; set; }
        }
        public class LaptopStoreContext : DbContext
        {
            //ctor for constructro


            public LaptopStoreContext(DbContextOptions<LaptopStoreContext> options):base(options)
            {
                
            }
            public DbSet<Laptop> laptops { get; set; }
            public DbSet<Vendor> vendors { get; set; }
            public DbSet<Purchase> purchases { get; set; }
        }
       
    
}
