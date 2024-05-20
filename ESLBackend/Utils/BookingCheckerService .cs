using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ESLBackend.Utils
{

    public class BookingCheckerService : IHostedService, IDisposable
    {
        private readonly ILogger<BookingCheckerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public BookingCheckerService(ILogger<BookingCheckerService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Booking Checker Service is starting.");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Checking active bookings at: {time}", DateTimeOffset.Now);
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var now = DateTime.Now;
                var activeBookings = context.BookingRooms
                                            .Where(b => b.StartTime <= now && b.EndTime >= now)
                                            .ToList();

                foreach (var booking in activeBookings)
                {
                    _logger.LogInformation("Active Booking: {booking}", booking);
                    // Perform any additional actions on the active bookings if necessary
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Booking Checker Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
