﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pinturería_Acuarela.Models;

public partial class Order
{
    [Key]
    public long ID { get; set; }

    [Display(Name = "Vendedor")]
    public long UserID { get; set; }


    [Display(Name = "Estado")]
    public bool Status { get; set; }

    [Display(Name = "Comentario")]
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
