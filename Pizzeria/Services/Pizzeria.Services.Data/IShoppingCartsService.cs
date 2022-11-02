using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzeria.Services.Data
{
    public interface IShoppingCartsService
    {
        Task Buy(int id, string userId);
    }
}
