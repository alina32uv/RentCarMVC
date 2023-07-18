using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarApp.Entities
{

    [Table("RentInfo")]
    public class RentInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentInfoId { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public DateTime DateBring { get; set; }
        public DateTime DateReturn { get; set; }


       
       /* [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; }*/


/*
        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        [Required]
        public virtual Status Status { get; set; }*/





       /* [ForeignKey(nameof(Insurance))]
        public int InsuranceId { get; set; }
        [Required]
        public virtual Insurance Insurance { get; set; }

        */

       // [ForeignKey(nameof(Car))]
      
        //public virtual Car Car { get; set; }

    }
}
