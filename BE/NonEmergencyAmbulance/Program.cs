using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Persistence.Data;
using Persistence.Data.Data_Seed;
using DomainLayer.Models.Identity_Module;
using ServiceAbstraction;
using Service;
using DomainLayer.Contracts;
using Persistence.Repositories;

namespace NonEmergencyAmbulance
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<INurseRepository, NurseRepository>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<ITripRepository, TripRepository>();
            builder.Services.AddScoped<ITripService, TripService>();
            builder.Services.AddScoped<IGeocodingService, NominatimGeocodingService>();
            builder.Services.AddScoped<IDistanceService, HaversineDistanceService>();
            builder.Services.AddScoped<ITripPriceCalculator, TripPriceCalculator>();

            builder.Services.AddScoped<INurseService, NurseService>();
            builder.Services.AddScoped<IDriverService, DriverService>();
            builder.Services.AddScoped<IAmbulanceRepository, AmbulanceRepository>();
            builder.Services.AddScoped<IAmbulanceService, AmbulanceService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IDistanceService, HaversineDistanceService>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();
            builder.Services.AddScoped<IRatingService, RatingService>();
            builder.Services.AddScoped<IReportRepository, ReportRepository>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddHttpClient<IGeocodingService, NominatimGeocodingService>(client =>
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("NonEmergencyAmbulanceApp/1.0");
            });


            builder.Services.AddScoped<DataSeed>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<DataSeed>();
                    await seeder.IdentityDataSeedAsync();
                    await seeder.DataSeedAsync();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during database seeding.");
                }
            }

            app.Run();
        }
    }
}
