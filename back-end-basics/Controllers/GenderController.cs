using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BACKEND.Entities;
using BACKEND.Filters;
using BACKEND.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenderController:ControllerBase
    {
        private readonly IRepository _repository;
        private readonly WeatherForecastController _weatherForecastController;
        private readonly ILogger<GenderController> _logger;

        public GenderController(IRepository repository, WeatherForecastController weatherForecastController,
            ILogger<GenderController> logger)
        {
            this._repository = repository;
            this._weatherForecastController = weatherForecastController;
            this._logger = logger;
        }

        [HttpGet("guid")]
        public ActionResult<Guid> GetGuid()
        {
            return Ok( new { GUID_gender = _repository.getGuid(), GUID_weather = _weatherForecastController.getGuidWFC() });
        }


        [HttpGet]
        [HttpGet("list")]
        [ResponseCache(Duration = 60)] //60segundos
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ServiceFilter(typeof(MyActionFilter))]
        public ActionResult<List<Gender>> Get()
        {
            _logger.LogInformation("Mostrando los generos");

            return _repository.getAllGenders();
        }

        //[HttpGet("{Id:int}/{ValueDefault:bool=true}")]
        [HttpGet("{Id:int}")] // api/gender/1
        public async Task<ActionResult<Gender>> GetById(int Id,[FromHeader] string ValueDefault)
        {
            _logger.LogInformation($"Mostrando el genero por Id {Id}");

            var gender = await _repository.getById(Id);

            if(gender== null) {
                throw new ApplicationException($"Exc: No se encontro genero con el Id {Id}."); 
                _logger.LogWarning($"No se encontro el genero por Id {Id}");
                return NotFound();
            }

            return gender;
        }


        [HttpPost]
        public ActionResult Post([FromBody] Gender gender)
        {
            _repository.createGender(gender);
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Gender gender)
        {
            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public ActionResult Delete()
        {
            return NoContent();
        }
    }
}
