using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinturería_Acuarela.Models;

public partial class ProductBusiness
{
    [Required]
    public long ProductID { get; set; }

    [Required]
    public long BusinessID { get; set; }

    [DisplayName("Stock mínimo")]
    [Range(1, 10000, ErrorMessage = "Debes ingresar un stock mínimo entre 1 y 10.000")]
    public int? MinimumStock { get; set; }

    [Required(ErrorMessage = "Debes ingresar un stock")]
    [Range(0, 10000, ErrorMessage = "Debes ingresar un stock entre 0 y 10.000")]
    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
