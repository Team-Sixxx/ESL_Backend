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
using ESLBackend.Models;
using System.Security.Claims;




namespace ESLBackend.Utils
{

    public class BookingCheckerService : IHostedService, IDisposable
    {
        private readonly ILogger<BookingCheckerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Timer _timer;

        public BookingCheckerService(ILogger<BookingCheckerService> logger,
            IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _httpContextAccessor = httpContextAccessor;
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

                        var upc = context.Upcs
                       .FirstOrDefault(b => b.Id == template.Id);


                        var item = context.Items
                   .FirstOrDefault(b => b.Id == template.Id);


                        if (template != null && upc != null && item != null)
                        {
                            var newTemplate = new Models.Templates
                            {
                                // Assign properties from template
                                Id = template.Id,
                                CreatedBy = template.CreatedBy,
                                CreatedTime = template.CreatedTime,
                                LastUpdatedBy = template.LastUpdatedBy,
                                LastUpdatedTime = template.LastUpdatedTime,
                                ShopCode = template.ShopCode,
                                GoodsCode = template.GoodsCode,
                                GoodsName = template.GoodsName,
                                TemplateType = template.TemplateType,
                                Version = template.Version,
                                HashCode = template.HashCode,

                                // Assign properties from upc
                                Upcs = new List<Models.Templates.Upc>
        {
            new Models.Templates.Upc
            {
                Id = upc.Id,
                GoodsCode = upc.GoodsCode,
                TemplatesId = upc.TemplatesId
            }
        },

                                // Assign properties from item
                                Items = new List<Models.Templates.Item>
        {
            new Models.Templates.Item
            {
                Id = item.Id,
                ShopCode = item.ShopCode,
                GoodsCode = item.GoodsCode,
                GoodsName = item.GoodsName,
                Upc1 = meetingRoom.Name,
                Upc2 = booking.EndTime.ToString("dddd, MMMM dd, yyyy 'at' hh:mm tt"),
                Upc3 = booking.User,
                Price1 = item.Price1,
                Price2 = item.Price2,
                Price3 = item.Price3,
                Origin = item.Origin,
                Spec = item.Spec,
                Unit = item.Unit,
                Raid = item.Raid,
                SalTimeStart = item.SalTimeStart,
                SalTimeEnd = item.SalTimeEnd,
                PriceClerk = item.PriceClerk,
                TemplatesId = item.TemplatesId
            }
        }
                            };




                            //var status = CallPostandPatchGoodsController(newTemplate).GetAwaiter().GetResult();

                            var status = "false";

                            if(status == "success")
                            {
                                booking.isLive = true;
                                context.SaveChanges();

                            } else
                            {
                                booking.isLive = false;
                                context.SaveChanges();

                            }

                          



                        }


                    }


                }
            }
        }





        public class Token
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
            public string body { get; set; }
            
        }
        private async Task<string> CallPostandPatchGoodsController(Models.Templates template)
        {
            var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            var token = await getToken();

            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogError("Token is missing");
                    return "Token is missing";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Missing token", ex.Message);
                return "Missing token: " + ex.Message;
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
                HttpResponseMessage response = await client.PostAsync("http://162.62.125.25:5003/api/Goods/save", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                _logger.LogInformation(responseBody);

              


                var tokenResponse = JsonSerializer.Deserialize<Token>(responseBody);

                if (tokenResponse?.Message == "success")
                {
                    _logger.LogInformation("PostandPatchGoods successful: {Message}", tokenResponse.Message);
                    return "success";
                    
                }
                else
                {
                    _logger.LogError("PostandPatchGoods failed: {Message}", tokenResponse?.Message);
                    return "failed";
                    
                }
            }
        



            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while calling PostandPatchGoods: {Message}", ex.Message);
                return "Exception occurred while calling PostandPatchGoods: " + ex.Message;
            }
        }




















        private static readonly HttpClient client = new HttpClient();

        private async Task<string> getToken()
        {
         
     


            try
            {

                using StringContent jsonContent = new(
      JsonSerializer.Serialize(new
      {

          password = 12345678,
          username = "testing"

      }),
      Encoding.UTF8,
      "application/json");

                using HttpResponseMessage response = await client.PostAsync("http://162.62.125.25:5003/api/login", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);


                Token? token = JsonSerializer.Deserialize<Token>(responseBody);



                return token?.body;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while calling PostandPatchGoods: {Message}", ex.Message);
                return null;
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
