using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pinturería_Acuarela.Models;

public partial class Order
{
    [Key]
    public long ID { get; set; }

    [Display(Name = "Vendedor")]
    public string UserID { get; set; } = null!;


    [Display(Name = "Estado")]
    public bool Status { get; set; }

    [Display(Name = "Comentario")]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<ProductOrder> Products { get; set; } = null!;
}
