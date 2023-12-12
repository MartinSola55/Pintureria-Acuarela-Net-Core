using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pintureria_Acuarela.Models;

public partial class ProductSale
{
    [Required]
    public long ProductID { get; set; }

    [Required]
    public long SaleID { get; set; }

    [DisplayName("Cantidad")]
    [Range(1, 10000, ErrorMessage = "Debes ingresar una cantidad entre 1 y 10.000")]
    public int Quantity { get; set; }

    public DateTime? DeletedAt { get; set; }


    public virtual Sale Sale { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
