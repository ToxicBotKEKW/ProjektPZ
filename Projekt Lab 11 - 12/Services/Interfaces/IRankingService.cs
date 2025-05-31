using Projekt_Lab_11___12.Models.ViewModels;

namespace Projekt_Lab_11___12.Services.Interfaces
{
    public interface IRankingService
    {
        public Task<RankingViewModel> RankingViewModel(int page = 1, int pageSize = 25);
    }
}
