using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace helping_hand.Models.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Not a valid phone number.")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [PasswordTooShort(6)]
        [PasswordRequiresUpper]
        [PasswordRequiresLower]
        [PasswordRequiresDigit]
        [PasswordRequiresNonAlphanumeric]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public ICollection<string> Roles { get; set; }
    }

    class PasswordTooShortAttribute : MinLengthAttribute
    {
        public PasswordTooShortAttribute(int length) : base(length)
        {
            ErrorMessage = $"Passwords must be at least {length} characters.";
        }
    }

    class PasswordRequiresUpperAttribute : ValidationAttribute
    {
        public PasswordRequiresUpperAttribute()
        {
            ErrorMessage = "Passwords must have at least one uppercase (A-Z).";
        }

        public override bool IsValid(object value)
        {
            return (value as string).Any(char.IsUpper);
        }
    }

    class PasswordRequiresLowerAttribute : ValidationAttribute
    {
        public PasswordRequiresLowerAttribute()
        {
            ErrorMessage = "Passwords must have at least one lowercase (a-z).";
        }

        public override bool IsValid(object value)
        {
            return (value as string).Any(char.IsLower);
        }
    }

    class PasswordRequiresDigitAttribute : ValidationAttribute
    {
        public PasswordRequiresDigitAttribute()
        {
            ErrorMessage = "Passwords must have at least one digit (0-9).";
        }

        public override bool IsValid(object value)
        {
            return (value as string).Any(char.IsDigit);
        }
    }

    class PasswordRequiresNonAlphanumericAttribute : ValidationAttribute
    {
        public PasswordRequiresNonAlphanumericAttribute()
        {
            ErrorMessage = "Passwords must have at least one non alphanumeric character.";
        }

        public override bool IsValid(object value)
        {
            return !(value as string).All(char.IsLetterOrDigit);
        }
    }
}
