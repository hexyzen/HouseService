using HouseService.Accessors.Entities;
using HouseService.Accessors.Entities.Dtos;
using HouseService.Managers.Exceptions;
using HouseService.Managers.Interfaces;

namespace HouseService.Managers.Managers
{
    public class DogManager : IDogManager
    {
        private readonly IDogAccessor _dogAccessor;

        public DogManager(IDogAccessor dogAccessor)
        {
            _dogAccessor = dogAccessor;
        }

        public async Task<IEnumerable<Dog>> GetAllDogsAsync()
        {
            return await _dogAccessor.GetAllDogsAsync();
        }

        public async Task<Dog> GetDogByIdAsync(int id)
        {
            return await _dogAccessor.GetDogByIdAsync(id);
        }

        public async Task CreateDogAsync(Dog dog)
        {
            if (dog is null)
                throw new EntityNotFoundException(typeof(Dog));
            await _dogAccessor.CreateDogAsync(dog);
        }

        public async Task UpdateDogAsync(Dog dog)
        {
            if (dog is null)
                throw new EntityNotFoundException(typeof(Dog));
            await _dogAccessor.UpdateDogAsync(dog);
        }

        public async Task DeleteDogAsync(int id)
        {
            try
            {
                await _dogAccessor.DeleteDogAsync(id);
            }
            catch
            {
                throw new EntityNotFoundException(typeof(Dog));
            }
        }

        public async Task<IEnumerable<Dog>> GetDogsAsync(DogsFilterPaginationDto dogsFilterPaginationDto)
        {
            return await _dogAccessor.GetDogsAsync(dogsFilterPaginationDto);
        }
    }
}