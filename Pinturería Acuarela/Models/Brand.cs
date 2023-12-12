using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pintureria_Acuarela.Models;

public partial class Brand
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes ingresar un nombre")]
    [Display(Name = "Nombre")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Debes ingresar un nombre de menos de 100 caracteres")]
    [RegularExpression(@"^[a-zA-Z\u00C0-\u017F\s']+$", ErrorMessage = "Ingrese un nombre válido")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Debes seleccionar si está o no habilitada")]
    [Display(Name = "Habilitada")]
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;

}
