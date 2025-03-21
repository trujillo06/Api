using TortilleriaSucursales.Models;

namespace TortilleriaSucursales.Repositories
{
    public interface ISucursalRepository
    {
        Task<IEnumerable<Sucursal>> GetAll();
        Task<Sucursal?> GetById(int id);
        Task Add(Sucursal sucursal);
        Task Update(Sucursal sucursal);
        Task Delete(int id);
    }
}
