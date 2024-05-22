using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ESLBackend.Controllers;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;



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
                                            .Where(b => b.StartTime <= now && b.EndTime >= now && b.isLive == false)
                                            .ToList();

                foreach (var booking in activeBookings)
                {
                    _logger.LogInformation("Active Booking: {booking}", booking);
                    // Perform any additional actions on the active bookings if necessary

                    var meetingRoom = context.MeetingRooms
                         .FirstOrDefault(b => b.Id == booking.MeetingRoomId);

                    if (meetingRoom != null)
                    {

                        var template = context.Templates
                        .FirstOrDefault(b => b.Id == meetingRoom.templateId);

                        if (template != null)
                        {
                            CallPostandPatchGoodsController(template).GetAwaiter().GetResult();

                            booking.isLive = true;
                            context.SaveChanges();



                        }


                    }


                }
            }
        }





        public class Token
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        private async Task CallPostandPatchGoodsController(Models.Templates template)
        {
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var session = httpContextAccessor.HttpContext.Session;
            var token = session.GetString("token");

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token is missing");
                return;
            }

            Models.PostTemplates postTemplate = Models.Templates.MappedTemplate(template);
            string serializedJson = JsonSerializer.Serialize(postTemplate);

            _logger.LogInformation("JSON Content: {serializedJson}", serializedJson);

            using StringContent jsonContent = new(
              JsonSerializer.Serialize(postTemplate),
              Encoding.UTF8,
              "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:5000/api/Goods", jsonContent); // Adjust the URL to your actual endpoint
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(responseBody);

                var tokenResponse = JsonSerializer.Deserialize<Token>(responseBody);

                if (tokenResponse?.Message == "success")
                {
                    _logger.LogInformation("PostandPatchGoods successful: {Message}", tokenResponse.Message);
                }
                else
                {
                    _logger.LogError("PostandPatchGoods failed: {Message}", tokenResponse?.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while calling PostandPatchGoods: {Message}", ex.Message);
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
