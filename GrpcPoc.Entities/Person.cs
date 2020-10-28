using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace GrpcPoc.Entities
{
    public class Person : EntityBase
    {
        [DataMember]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name exceeds {1} characters.")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Middle Name")]
        [StringLength(150, ErrorMessage = "Middle name exceeds {1} characters.")]
        public string MiddleName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(150, ErrorMessage = "Last name exceeds {1} characters.")]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DateOfBirth { get; set; }
    }
}
