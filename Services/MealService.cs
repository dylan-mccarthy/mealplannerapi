using MealPlannerApi.Data;
using MealPlannerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Services
{
    public class MealService
    {
        private readonly MealPlannerContext _context;

        public MealService(MealPlannerContext context)
        {
            _context = context;
        }

        public async Task<List<Meal>> GetMealsAsync()
        {
            return await _context.Meals.ToListAsync();
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            var result = await _context.Meals.FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException();
            }
            return result;
        }

        public async Task<List<Meal>> GetMealsForDateAsync(string date)
        {
            return await _context.Meals
                .Where(m => m.Date == date)
                .ToListAsync();
        }

        public async Task AddMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(Meal meal)
        {
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
        }
    }
}
