using System.ComponentModel.DataAnnotations;

namespace GymMembershipSystem.Models;

public class MemberRegistration
{
    [Required(ErrorMessage = "Ad Soyad zorunludur")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Telefon zorunludur")]
    [MinLength(10, ErrorMessage = "Telefon numarası en az 10 karakter olmalıdır")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "TC Kimlik No zorunludur")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "TC Kimlik No sadece rakamlardan oluşmalıdır")]
    public string TcKimlikNo { get; set; } = "";

    public string MembershipId { get; set; } = "";
}

