//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;
    using System.Web.Security;

    public partial class tbl_Employee
    {
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(12)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(12)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("IsAlreadySignedUpEmployee", "Employee", ErrorMessage = "EmailId already exists in database.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [DisplayName("Password")]
        [MembershipPassword(
         MinRequiredNonAlphanumericCharacters = 1,
         MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
         ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
         MinRequiredPasswordLength = 6
)]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [NotMapped]
        public string ConfirmPassword
        {
            get;
            set;
        }
        [MinimumAge(18,ErrorMessage ="You must be atleast 18 years of age ")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> DOB { get; set; }
    }
}
