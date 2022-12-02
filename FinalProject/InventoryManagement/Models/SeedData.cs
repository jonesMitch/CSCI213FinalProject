using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.Data;
using System;
using System.Linq;

namespace InventoryManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvidor)
        {
            using (var context = new InventoryManagementContext(
                serviceProvidor.GetRequiredService<DbContextOptions<InventoryManagementContext>>()))
            {
                if (context.Computer.Any())
                    return;

                context.Computer.AddRange(
                    new Computer
                    {
                        manufacturerSerialNumber = 1234,
                        OfficeRoomNumber = "116",
                        OfficeLocation = "QBB",
                        ComputerSpecification = "SCL-QUBB0116-29",
                        OperatingSystem = "Windows 10 21H2",
                        OwnerName = "Mitch Jones",
                        InstallationDate = new DateTime(2017, 7, 26),
                        Price = 799m
                    },
                    new Computer
                    {
                        manufacturerSerialNumber = 1234,
                        OfficeRoomNumber = "116",
                        OfficeLocation = "QBB",
                        ComputerSpecification = "SIC-QUBB0116-01",
                        OperatingSystem = "Windows 10 21H2",
                        OwnerName = "Oksana Myronovych",
                        InstallationDate = new DateTime(2017, 7, 26),
                        Price = 799m
                    },
                    new Computer
                    {
                        manufacturerSerialNumber = 4567,
                        OfficeRoomNumber = "150",
                        OfficeLocation = "QBB",
                        ComputerSpecification = "SCL-QUBB0150-44",
                        OperatingSystem = "MacOS 12 Monterey",
                        OwnerName = "Billy-Bob Joe",
                        InstallationDate = new DateTime(2021, 10, 25),
                        Price = 1299m
                    }
                );
                context.SaveChanges();
            }
        }
    }
}