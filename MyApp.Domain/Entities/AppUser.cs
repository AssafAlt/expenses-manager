using MyApp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(10, ErrorMessage = "First name cannot exceed 10 characters.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string? PhoneNumber { get; set; }
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
