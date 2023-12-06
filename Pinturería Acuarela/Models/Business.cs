using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinturería_Acuarela.Models;

public partial class Business
{
    [Key]
    public long ID { get; set; }

    [Display(Name = "Dirección")]
    public string? Address { get; set; }
}
