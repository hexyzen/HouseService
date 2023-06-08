using Microsoft.EntityFrameworkCore;
using HouseService.Managers.Interfaces;
using HouseService.Accessors.Context;
using HouseService.Accessors.Entities;
using HouseService.Accessors.Entities.Dtos;

namespace HouseService.Managers.Accessors
{
    public class DogAccessor : IDogAccessor
    {
        private readonly HouseServiceContext _context;

        public DogAccessor(HouseServiceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Dog>> GetAllDogsAsync()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<Dog> GetDogByIdAsync(int id)
        {
            return await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id);
        }

        public async Task CreateDogAsync(Dog dog)
        {
            _context.Dogs.Add(dog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDogAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDogAsync(int id)
        {
            var dog = await _context.Dogs.FirstOrDefaultAsync(dog => dog.Id == id);
            if (dog != null)
            {
                _context.Dogs.Remove(dog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Dog>> GetDogsAsync(DogsFilterPaginationDto dogsFilterPaginationDto)
        {
            IQueryable<Dog> query = _context.Dogs;

            if (!string.IsNullOrEmpty(dogsFilterPaginationDto.attribute))
            {
                switch (dogsFilterPaginationDto.attribute.ToLower())
                {
                    case "name":
                        query = (dogsFilterPaginationDto.order.ToLower() == "desc") ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name);
                        break;
                    case "color":
                        query = (dogsFilterPaginationDto.order.ToLower() == "desc") ? query.OrderByDescending(d => d.Color) : query.OrderBy(d => d.Color);
                        break;
                    case "tail_length":
                        query = (dogsFilterPaginationDto.order.ToLower() == "desc") ? query.OrderByDescending(d => d.TailLength) : query.OrderBy(d => d.TailLength);
                        break;
                    case "weight":
                        query = (dogsFilterPaginationDto.order.ToLower() == "desc") ? query.OrderByDescending(d => d.Weight) : query.OrderBy(d => d.Weight);
                        break;
                }
            }

            if (dogsFilterPaginationDto.pageNumber > 0 && dogsFilterPaginationDto.pageSize > 0)
            {
                query = query.Skip((dogsFilterPaginationDto.pageNumber - 1) * dogsFilterPaginationDto.pageSize).Take(dogsFilterPaginationDto.pageSize);
            }

            return await query.ToListAsync();
        }
    }
}
