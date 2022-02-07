using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TaskTime.Models
{
    public class TaskContext : DbContext
    {
        //Constructor
        public TaskContext(DbContextOptions<DbContext> options) : base(options)
        {
            //Leave blank for now
        }

        public DbSet<ApplicationResponse> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }

        // Seed data
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // seed the four categories (Home, School, Work, Church) into the categories table
            mb.Entity<Category>().HasData(
                new Category
                { CategoryId = 1, CategoryName = "Home"  },
                new Category
                { CategoryId = 2, CategoryName = "School" },
                new Category
                { CategoryId = 3, CategoryName = "Work" },
                new Category
                { CategoryId = 4, CategoryName = "Church" }
                );

            // Seed initial response
            mb.Entity<ApplicationResponse>().HasData(
                new ApplicationResponse
                {
                    AppResponseId = 1,
                    CategoryID = 1,
                    Task = "Eat Breakfast",
                    DueDate = "Feb 8th",
                    Quadrant = "Quadrant III",
                    Completed = false

                }
            );
        }
    }
}
