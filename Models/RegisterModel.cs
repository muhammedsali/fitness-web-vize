using System.ComponentModel.DataAnnotations;

namespace GymMembershipSystem.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Ad Soyad zorunludur")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Şifre zorunludur")]
    [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; } = "";
}

