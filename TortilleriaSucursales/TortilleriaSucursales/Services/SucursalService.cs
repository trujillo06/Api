using TortilleriaSucursales.Dtos;
using TortilleriaSucursales.Models;
using TortilleriaSucursales.Repositories;

namespace TortilleriaSucursales.Services
{
    public class SucursalService : ISucursalService
    {
        private readonly ISucursalRepository _repository;

        public SucursalService(ISucursalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SucursalDTO>> GetAllSucursales()
        {
            var sucursales = await _repository.GetAll();
            return sucursales.Select(s => new SucursalDTO
            {
                Id_Sucursal = s.Id_Sucursal,
                Nombre = s.Nombre,
                Direccion = s.Direccion,
                Telefono_Contacto = s.Telefono_Contacto,
                Nombre_Encargado = s.Nombre_Encargado
            });
        }

        public async Task<SucursalDTO?> GetSucursalById(int id)
        {
            var sucursal = await _repository.GetById(id);
            return sucursal != null ? new SucursalDTO
            {
                Id_Sucursal = sucursal.Id_Sucursal,
                Nombre = sucursal.Nombre,
                Direccion = sucursal.Direccion,
                Telefono_Contacto = sucursal.Telefono_Contacto,
                Nombre_Encargado = sucursal.Nombre_Encargado
            } : null;
        }

        public async Task AddSucursal(SucursalDTO sucursalDto)
        {
            var sucursal = new Sucursal
            {
                Nombre = sucursalDto.Nombre,
                Direccion = sucursalDto.Direccion,
                Telefono_Contacto = sucursalDto.Telefono_Contacto,
                Nombre_Encargado = sucursalDto.Nombre_Encargado
            };
            await _repository.Add(sucursal);
        }

        public async Task UpdateSucursal(int id, SucursalDTO sucursalDto)
        {
            var sucursal = await _repository.GetById(id);
            if (sucursal != null)
            {
                sucursal.Nombre = sucursalDto.Nombre;
                sucursal.Direccion = sucursalDto.Direccion;
                sucursal.Telefono_Contacto = sucursalDto.Telefono_Contacto;
                sucursal.Nombre_Encargado = sucursalDto.Nombre_Encargado;
                await _repository.Update(sucursal);
            }
        }

        public async Task DeleteSucursal(int id)
        {
            await _repository.Delete(id);
        }
    }
}
