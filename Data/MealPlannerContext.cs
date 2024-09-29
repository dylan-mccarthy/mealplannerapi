using Microsoft.EntityFrameworkCore;
using MealPlannerApi.Models;

namespace MealPlannerApi.Data
{
    public class MealPlannerContext : DbContext
    {
        public DbSet<Meal> Meals { get; set; }

        public DbSet<SettingsModel> Settings { get; set; }

        public MealPlannerContext(DbContextOptions<MealPlannerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration (if needed)
        }
    }
}
