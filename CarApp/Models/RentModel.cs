using CarApp.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarApp.Models
{
    public class RentModel
    {
        [Key]
        public int RentInfo { get; set; } 
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public DateTime DateBring { get; set; }
        public DateTime DateReturn { get; set; }
        public string UserId { get; set; }
        


        public string DateBringDate => DateBring.ToString("MM/dd/yyyy");
        public string DateBringTime => DateBring.ToString("hh:mm tt");
        public string DateReturnDate => DateReturn.ToString("MM/dd/yyyy");
        public string DateReturnTime => DateReturn.ToString("hh:mm tt");



    }
}
