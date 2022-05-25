using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopPCStore.Models
{
    //add-migration -Context LaptopStoreContext frist
    //update-database -Context LaptopStoreContext

    public class Laptop                     //USE PROP to generate 
    {
        [Key]
        public int lap_id { get; set; }
        [Display(Name = "Laptop Name")]
        public string lap_name { get; set; }
        [Display(Name = "Category")]
        public string lap_cat { get; set; }
        [Display(Name = "RAM")]
        public string lap_ram { get; set; }
        [Display(Name = "CPU")]
        public string lap_cpu { get; set; }
        [Display(Name = "GPU")]
        public string lap_gpu { get; set; }
        [Display(Name = "Size")]
        public string lap_disp_size { get; set; }
        [Display(Name = "Resolution")]
        public string lap_disp_res { get; set; }
        [Display(Name = "Refresh Rate")]
        public string lap_disp_rr { get; set; }
        [Display(Name = "Battery")]
        public string lap_battrey { get; set; }
        [Display(Name = "Storage")]
        public string lap_storage { get; set; }
            //public int lap_quantity { get; set; }
    }

    public class Vendor
    {
        [Key]
        public int ven_id { get; set; }
        [Display(Name = "Vendor Name")]
        public string ven_name { get; set; }
        [Display(Name = "Phone Number")]
        public string ven_number { get; set; }
        [Display(Name = "Email")]
        public string ven_mail { get; set; }
        [Display(Name = "Address")]
        public string ven_address { get; set; }      

    }

    public class Purchase
    {
        [Key]
        public int purchase_id { get; set; }

        [ForeignKey("fk2")]
        [Display(Name = "Vendor Name")]
        public int? ven_id { get; set; }
        public Vendor fk2 { get; set; }

        [ForeignKey("fk3")]
        [Display(Name = "Laptop Name")]
        public int? lap_id { get; set; }
        public Laptop fk3 { get; set; }
        [Display(Name = "Quantity")]
        public int purchase_quantity { get; set; }
        [Display(Name = "Date")]
        public DateTime purchase_date { get; set; }
        [Display(Name = "Cost")]
        public int purchase_cost { get; set; }
    }


    public class Invoice
    {
        [Key]
        public int invoice_id { get; set; }
        public string userID { get; set; } //httpcontext.current.user.identity.name

        [ForeignKey("fk6")]
        [Display(Name = "Laptop Name")]
        public int lap_id { get; set; }
        public Laptop fk6 { get; set; }
        [Display(Name = "Quantity")]
        public int quantity { get; set; }
        [Display(Name = "Name")]
        public string inv_name { get; set; }
        [Display(Name = "Phone")]
        public string inv_phone { get; set; }
        [Display(Name = "Email")]
        public string inv_mail { get; set; }
        [Display(Name = "Address")]
        public string inv_address { get; set; }
    }


    public class Inventory
    {
        //[Key]
        //public int inv_id { get; set; }
        [Key]
        [ForeignKey("fk4")]
        [Display(Name = "Laptop Name")]
        public int lap_id { get; set; }
        public Laptop fk4 { get; set; }
        [Display(Name = "Quantity")]

        public int quantity { get; set; }

    }

    public class LaptopStoreContext : DbContext
        {
            //ctor for constructors


            public LaptopStoreContext(DbContextOptions<LaptopStoreContext> options):base(options)
            {
                
            }
            public DbSet<Laptop> laptops { get; set; }
            public DbSet<Vendor> vendors { get; set; }
            public DbSet<Purchase> purchases { get; set; }
            public DbSet<Invoice> invoices { get; set; }
            public DbSet<Inventory> inventories { get; set; }
        }
       
    
}
