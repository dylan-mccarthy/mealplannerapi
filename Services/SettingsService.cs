using MealPlannerApi.Data;
using MealPlannerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlannerApi.Services
{
    public class SettingsService
    {
        private readonly MealPlannerContext _context;

        public SettingsService(MealPlannerContext context)
        {
            _context = context;
        }

        public async Task<SettingsModel> GetSettingsAsync()
        {
            var result = await _context.Settings.FirstOrDefaultAsync();
            if (result == null)
            {
                //Create default settings if none exist
                result = new SettingsModel
                {
                    KilojoulesGoal = 8000,
                    ProteinGoal = 100,
                    CarbsGoal = 300,
                    FatGoal = 70
                };
                _context.Settings.Add(result);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task UpdateSettingsAsync(SettingsModel settings)
        {

            // Check if there are any settings in the database
            var existingSettings = await _context.Settings.FirstOrDefaultAsync();
            if (existingSettings == null)
            {
                // If there are no settings, add the new settings
                _context.Settings.Add(settings);
            }
            else
            {
                // If there are settings, update the existing settings
                existingSettings.KilojoulesGoal = settings.KilojoulesGoal;
                existingSettings.ProteinGoal = settings.ProteinGoal;
                existingSettings.CarbsGoal = settings.CarbsGoal;
                existingSettings.FatGoal = settings.FatGoal;
                _context.Settings.Update(existingSettings);
            }
            await _context.SaveChangesAsync();
        }
    }
}