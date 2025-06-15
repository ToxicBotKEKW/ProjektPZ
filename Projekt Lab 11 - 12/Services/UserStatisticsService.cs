using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserStatisticsService(Projekt_Lab_11___12Context context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<UserStatisticsViewModel> UserStatisticsViewModel(String userId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;

            var searchUser = await _context.Users
                .Include(x => x.IronMine)
                .Include(x => x.GoldMine)
                .Include(x => x.DiamondMine)
                .Include(x => x.PickaxesEq)
                .FirstOrDefaultAsync(x => x.Id == userId);

            UserStatViewModel userStatViewModel = new UserStatViewModel()
            {
                Iron = searchUser.Iron,
                Gold = searchUser.Gold,
                Diamond = searchUser.Diamond,
                Emerald = user.Emerald,
                Email = searchUser.Email,
                IronMine = searchUser.IronMine,
                GoldMine = searchUser.GoldMine,
                DiamondMine = searchUser.DiamondMine,
                PickaxesEq = searchUser.PickaxesEq
            };

            UserStatisticsViewModel viewModel = new UserStatisticsViewModel()
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                UserStatViewModel = userStatViewModel
            };

            return viewModel;
        }
    }
}
