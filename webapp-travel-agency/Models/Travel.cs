using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp_travel_agency.Models
{
    public class Travel
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Il nome non può avere più di 50 caratteri")]
        public string Name { get; set; }
        public string Photo { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Range(1, 999, ErrorMessage = "La durata del viaggio non può essere inferiore a 1")]
        public int Day { get; set; }
        public int Destination { get; set; }

        [Range(1, 999, ErrorMessage = "Il prezzo non può essere inferiore a 1")]
        public int Price { get; set; }

        public Travel()
        {

        }
        public Travel(string name, string description, int day, int price)
        {
            Name = name;
            Description = description;
            Day = day;
            Price = price;
        }
    }
}
