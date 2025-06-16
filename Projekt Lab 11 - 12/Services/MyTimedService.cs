using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projekt_Lab_11___12.Data;
using Projekt_Lab_11___12.Models.Entities;

namespace Projekt_Lab_11___12.Services
{
    public class MyTimedService : BackgroundService
    {
        private readonly ILogger<MyTimedService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public MyTimedService(
            ILogger<MyTimedService> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<Projekt_Lab_11___12Context>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();

            var users = context.Users
                .Include(u => u.IronMine)
                    .ThenInclude(b => b.BonusClicks)
                .Include(u => u.GoldMine)
                    .ThenInclude(b => b.BonusClicks)
                .Include(u => u.DiamondMine)
                    .ThenInclude(b => b.BonusClicks)
                .ToList();

            foreach (var user in users)
            {
                if (user.IronMine?.BonusClicks != null)
                    user.IronMine.BonusClicks.CurrentClicks = Math.Min(100, user.IronMine.BonusClicks.CurrentClicks + 5);

                if (user.GoldMine?.BonusClicks != null)
                    user.GoldMine.BonusClicks.CurrentClicks = Math.Min(100, user.GoldMine.BonusClicks.CurrentClicks + 3);

                if (user.DiamondMine?.BonusClicks != null)
                    user.DiamondMine.BonusClicks.CurrentClicks = Math.Min(100, user.DiamondMine.BonusClicks.CurrentClicks + 1);
            }

            context.SaveChanges();
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(stoppingToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
