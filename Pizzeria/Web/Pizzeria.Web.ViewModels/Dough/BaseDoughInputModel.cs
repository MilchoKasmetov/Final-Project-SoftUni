namespace Pizzeria.Web.ViewModels.Dough
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class BaseDoughInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
