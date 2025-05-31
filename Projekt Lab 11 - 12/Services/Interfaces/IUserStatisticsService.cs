using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IUserStatisticsService
    {
        public Task<UserStatisticsViewModel> UserStatisticsViewModel(String userId);
    }
}
