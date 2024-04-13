using Hope.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AiPostController : ControllerBase
    {
        private readonly IAiPostServices services;

        public AiPostController(IAiPostServices services)
        {
            this.services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostOfPeopleByTown(string town)
        {

            return Ok(await services.GetPostOfPeopleByTown(town));  
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfPeopleByAge(int age)
        {

            return Ok(await services.GetPostOfPeopleByAge(age));
        }[HttpGet]
        public async Task<IActionResult> GetPostOfPeopleByTownandAge(int age,string town)
        {

            return Ok(await services.GetPostOfPeopleByTownandAge(age,town));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfThingsByTown(string town)
        {

            return Ok(await services.GetPostOfThingsByTown(town));
        }

        [HttpGet]
        public async Task<IActionResult> GetHospitalsByCity(string city)
        {

            return Ok(await services.GetHospitalsByCity(city));
        }


    }
}
