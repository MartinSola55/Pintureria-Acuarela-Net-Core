using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Pinturería_Acuarela.Models;

public partial class Sale
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long ID { get; set; }

    [Display(Name = "Vendedor")]
    public string UserID { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    [AllowNull]
    public virtual ApplicationUser User { get; set; } = new();

    public virtual ICollection<ProductSale> Products { get; set; } = new List<ProductSale>();
}
