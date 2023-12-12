using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pintureria_Acuarela.Models;

public partial class ProductOrder
{
    [Required]
    public long ProductID { get; set; }

    [Required]
    public long OrderID { get; set; }

    [DisplayName("Cantidad")]
    [Range(1, 10000, ErrorMessage = "Debes ingresar una cantidad entre 1 y 10.000")]
    public int Quantity { get; set; }

    public bool Status { get; set; }

    public int QuantitySend { get; set; }

    public long? BusinessID { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Business Business { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
