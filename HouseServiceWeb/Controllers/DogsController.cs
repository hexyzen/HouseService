using HouseService.Accessors.Entities;
using HouseService.Accessors.Entities.Dtos;
using HouseService.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseService.Web.Controllers
{
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly IDogManager _dogService;

        public DogsController(IDogManager dogService)
        {
            _dogService = dogService;
        }

        [HttpGet("ping")]
        public async Task<ActionResult> Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetDogs([FromQuery] DogsFilterPaginationDto dogsFilterPaginationDto)
        {
            var dogs = await _dogService.GetDogsAsync(dogsFilterPaginationDto);
            return Ok(dogs);
        }

        [HttpPost("dog")]
        public async Task<IActionResult> CreateDog([FromBody] Dog dog)
        {
           await _dogService.CreateDogAsync(dog);
           return Ok(dog);
        }
    }
}
