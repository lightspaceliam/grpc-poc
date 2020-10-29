using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

/// <summary>
/// Duplication of model Person here and not backed with inheritance or an interface. But this is out of scope for the purpose of exploring gRPC.
/// </summary>
namespace GrpcPoc.Api.Models
{
    public class PersonDto
    {
        [DataMember]
        [Display(Name = "Id")]
        public int Id { get; set; }

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
