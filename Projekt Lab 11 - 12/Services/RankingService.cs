using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;
using Projekt_Lab_11___12.Models.ViewModels;
using Projekt_Lab_11___12.Services.Interfaces;

namespace Projekt_Lab_11___12.Services
{
    public class RankingService : IRankingService
    {
        private readonly Projekt_Lab_11___12Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public RankingService(Projekt_Lab_11___12Context context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<RankingViewModel> RankingViewModel(int page = 1, int pageSize = 25)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return null;
            }

            var userList = await _context.Users.ToListAsync();

            var pagedItems = userList
                .OrderByDescending(u => u.Iron + u.Gold + u.Diamond)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            List<UserViewModel> users = new List<UserViewModel>();

            foreach (var userItem in pagedItems)
            {
                users.Add(new UserViewModel
                {
                    UserId = userItem.Id,
                    Emial = userItem.Email,
                    Iron = userItem.Iron,
                    Gold = userItem.Gold,
                    Diamond = userItem.Diamond,
                    Emerald = user.Emerald
                });
            }


            RankingViewModel rankingViewModel = new RankingViewModel()
            {
                Iron = user.Iron,
                Gold = user.Gold,
                Diamond = user.Diamond,
                Emerald = user.Emerald,
                UserList = users,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)users.Count / pageSize)
            };

            return rankingViewModel;
        }
    }
}
