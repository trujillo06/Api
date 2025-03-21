using Microsoft.EntityFrameworkCore;
using TortilleriaSucursales.Data;
using TortilleriaSucursales.Models;

namespace TortilleriaSucursales.Repositories
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly ApplicationDbContext _context;

        public SucursalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sucursal>> GetAll()
        {
            return await _context.Sucursales.ToListAsync();
        }

        public async Task<Sucursal?> GetById(int id)
        {
            return await _context.Sucursales.FindAsync(id);
        }

        public async Task Add(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Sucursal sucursal)
        {
            _context.Sucursales.Update(sucursal);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal != null)
            {
                _context.Sucursales.Remove(sucursal);
                await _context.SaveChangesAsync();
            }
        }
    }
}
