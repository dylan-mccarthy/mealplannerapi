using System.ComponentModel.DataAnnotations;
namespace MealPlannerApi.Models;
public class SettingsModel
{
    [Key]
    public int Id { get; set; }
    public int KilojoulesGoal { get; set; }
    public int ProteinGoal { get; set; }
    public int CarbsGoal { get; set; }
    public int FatGoal { get; set; }
}