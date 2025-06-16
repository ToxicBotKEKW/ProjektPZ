using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IMineService
    {
        public Task<MineViewModel> MineViewModel();
		public Task<string> PointsForClick(ResourceType resourceType);
		public Task<string> BonusPointsForClick(ResourceType resourceType);
    }
}
