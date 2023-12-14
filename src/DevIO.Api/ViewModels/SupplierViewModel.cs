using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels;

public class SupplierViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(100, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 2)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The {0} field is required")]
    [StringLength(14, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 11)]
    public string? Document { get; set; }

    public int SupplierType { get; set; }

    public bool Active { get; set; }

    public AddressViewModel? Address { get; set; }

    public IEnumerable<ProductViewModel>? Products { get; set; }
}
