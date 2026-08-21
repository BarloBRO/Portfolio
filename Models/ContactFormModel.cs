using System.ComponentModel.DataAnnotations;

namespace BarloPortfolio.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a message.")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message must be at least 10 characters.")]
    public string Message { get; set; } = string.Empty;
}
