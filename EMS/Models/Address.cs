using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Models
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressID { get; set; }

        [Required]
        public string HouseName { get; set; }

        [Required]
        public string HouseNo { get; set; }

        [Required]
        public string RoadNo { get; set; }

        [Required]
        public string WardNo { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public int EmployeeID { get; set; } 
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
