﻿namespace Pizzeria.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Pizzeria.Data.Common.Models;

    public class Dough : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
