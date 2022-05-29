using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Models
{
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EducationID { get; set; }

        [Required]
        public int DegreeID { get; set; }
        [ForeignKey("DegreeID")]
        public virtual Degree Degree { get; set; }

        [Required]
        public string Institution { get; set; }

        [Required]
        public string PassingYear { get; set; }

        [Required]
        public string GPA { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
     
    }
}
