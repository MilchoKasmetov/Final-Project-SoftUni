namespace Pizzeria.Web.ViewModels.Dough
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseDoughInputModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
