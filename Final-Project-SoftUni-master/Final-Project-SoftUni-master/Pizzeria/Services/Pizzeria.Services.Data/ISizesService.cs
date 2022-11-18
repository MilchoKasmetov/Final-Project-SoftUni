﻿namespace Pizzeria.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Pizzeria.Web.ViewModels.Pizzas;
    using Pizzeria.Web.ViewModels.Sizes;

    public interface ISizesService
    {
        Task<ICollection<PizzaSizeInputModel>> GetSizesAsync();

        Task<ICollection<SizeViewModel>> GetAllSizesAsync();

        Task<ICollection<SizeViewModel>> ShowAllDeletedAsync();

        Task CreateSizeAsync(CreateSizeInputModel model);

        Task<EditSizeInputModel> GetForUpdateAsync(int id);

        Task UpdateAsync(int id, EditSizeInputModel input);

        Task Delete(int id);

        Task Restore(int id);
    }
}
