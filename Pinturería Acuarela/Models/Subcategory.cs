using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pintureria_Acuarela.Models;

public partial class Subcategory
{
    [Key]
    public long ID { get; set; }

    [Display(Name = "Descripción")]
    public string Description { get; set; } = null!;
}
