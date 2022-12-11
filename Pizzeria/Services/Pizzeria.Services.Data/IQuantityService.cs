namespace Pizzeria.Services.Data
{
    using System.Threading.Tasks;

    public interface IQuantityService
    {
        Task Increase(int id, string userId);
        Task Decrease(int id, string userId);
    }
}
