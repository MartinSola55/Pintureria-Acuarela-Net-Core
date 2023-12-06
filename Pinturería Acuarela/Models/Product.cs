using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pinturería_Acuarela.Models;

public partial class Product
{
    [Key]
    public long ID { get; set; }

    [Required(ErrorMessage = "Debes completar con una descripción")]
    [StringLength(255, ErrorMessage = "Ingresa una descripción menor de 255 caracteres")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Debes seleccionar una marca")]
    [DisplayName("Marca")]
    public long BrandID { get; set; }

    [AllowNull]
    [DisplayName("Categoría")]
    public long? CategoryID { get; set; }

    [AllowNull]
    [DisplayName("Subcategoría")]
    public long? SubcategoryID { get; set; }

    [AllowNull]
    [DisplayName("Capacidad")]
    public long? CapacityID { get; set; }

    [AllowNull]
    [DisplayName("Color")]
    public long? ColorID { get; set; }

    [Required(ErrorMessage = "Debes completar con un código interno")]
    [DisplayName("Código interno")]
    [StringLength(50, ErrorMessage = "Ingresa un código de menos de 50 caracteres")]
    public string InternalCode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    [AllowNull]
    public virtual Capacity? Capacity { get; set; } = null!;

    [AllowNull]
    public virtual Category? Category { get; set; } = null!;

    [AllowNull]
    public virtual Subcategory? Subcategory { get; set; } = null!;

    [AllowNull]
    public virtual Color? Color { get; set; } = null!;
}
