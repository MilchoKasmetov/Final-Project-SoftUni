﻿namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Pizzas;

    public interface ISizesService
    {
        Task<ICollection<PizzaSizeInputModel>> GetSizesAsync();
    }
}
