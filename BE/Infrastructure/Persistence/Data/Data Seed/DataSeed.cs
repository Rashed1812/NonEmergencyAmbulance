using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.Ambulance_Module;
using DomainLayer.Models.Identity_Module;
using DomainLayer.Models.Request_Module;
using DomainLayer.Models.Trip_Module;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data.Data_Seed
{
    public class DataSeed(
        ApplicationDbContext _DbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        ILogger<DataSeed> _logger
    ) : IDataSeed
    {
        public async Task DataSeedAsync()
        {
            var pendingMigrations = await _DbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _DbContext.Database.MigrateAsync();

            try
            {

                if (!_DbContext.Set<Ambulance>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Ambulance.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Ambulance>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Ambulance>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Patient>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Patient.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Patient>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Patient>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Driver>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Driver.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Driver>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Driver>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Nurse>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Nurse.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Nurse>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Nurse>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Admin>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Admin.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Admin>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Admin>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Request>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Request.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Request>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Request>().AddRangeAsync(list);
                }

                if (!_DbContext.Set<Trip>().Any())
                {
                    var data = File.OpenRead(@"..\Infrastructure\Persistence\Data\Data Seed\FilesData\Trip.json");
                    var list = await JsonSerializer.DeserializeAsync<List<Trip>>(data);
                    if (list is not null && list.Any())
                        await _DbContext.Set<Trip>().AddRangeAsync(list);
                }
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during data seeding: {ex.Message}");
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                // Step 1: Create Roles if not exist
                string[] roles = { "Admin", "Driver", "Nurse", "Patient" };
                foreach (var role in roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
                var fullPath = Path.GetFullPath(
                    Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\Infrastructure\Persistence\Data\Data Seed\FilesData\ApplicationUser.json")
                );

                Console.WriteLine($"Looking for seed file at: {fullPath}");

                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"Seed file not found: {fullPath}");
                    return;
                }


                var jsonData = await File.ReadAllTextAsync(fullPath);
                var userSeedList = JsonSerializer.Deserialize<List<ApplicatiosUserSeedModel>>(jsonData);

                if (userSeedList is null || !userSeedList.Any())
                {
                    Console.WriteLine("No user data found in seed file.");
                    return;
                }

                foreach (var seed in userSeedList)
                {
                    var existingUser = await _userManager.FindByEmailAsync(seed.Email);
                    if (existingUser is null)
                    {
                        var user = new ApplicationUser
                        {
                            FullName = seed.FullName,
                            UserName = seed.UserName,
                            Email = seed.Email,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true
                        };

                        var result = await _userManager.CreateAsync(user, seed.Password);

                        if (result.Succeeded)
                        {
                            await _userManager.AddToRoleAsync(user, seed.Role);
                            Console.WriteLine($"User created: {seed.Email} as {seed.Role}");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to create {seed.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"User already exists: {seed.Email}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding error: {ex.Message}");
            }
        }
    }
}