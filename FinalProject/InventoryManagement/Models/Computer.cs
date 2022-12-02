using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Computer
    {
        public int id { get; set; }
        public int manufacturerSerialNumber { get; set; }
        public string? OfficeRoomNumber { get; set; }
        public string? OfficeLocation { get; set; }
        public string? ComputerSpecification { get; set; }
        public string? OperatingSystem { get; set; }
        public string? OwnerName { get; set; }
        [Display(Name ="InstallationDate")][DataType(DataType.Date)]
        public DateTime InstallationDate { get; set; }
        public decimal Price { get; set; }
    }
}
