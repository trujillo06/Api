using TortilleriaSucursales.Dtos;

namespace TortilleriaSucursales.Services
{
    public interface ISucursalService
    {
        Task<IEnumerable<SucursalDTO>> GetAllSucursales();
        Task<SucursalDTO?> GetSucursalById(int id);
        Task AddSucursal(SucursalDTO sucursalDto);
        Task UpdateSucursal(int id, SucursalDTO sucursalDto);
        Task DeleteSucursal(int id);
    }
}
