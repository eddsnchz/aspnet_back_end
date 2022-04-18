using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Entities;
using BACKEND.Filters;
using BACKEND.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenderController:ControllerBase
    {
        private ILogger<GenderController> logger;
        private ApplicationDBContext context { get; }
        private readonly IMapper mapper;

        public GenderController(ILogger<GenderController> logger, ApplicationDBContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Gender.AsQueryable();
            await HttpContext.InsertParamsPaginationInHeader(queryable);
            var genders = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<GenderDTO>>(genders);
        }

        [HttpGet("{Id:int}")] //api/gender/1
        public async Task<ActionResult<GenderDTO>> GetById(int Id,[FromHeader] string ValueDefault)
        {
            var gender = await context.Gender.FirstOrDefaultAsync(x => x.Id == Id);

            if (gender == null)
            {
                return NotFound();
            }

            return mapper.Map<GenderDTO>(gender);
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateGenderDTO createGenderDTO)
        { 
            var gender = mapper.Map<Gender>(createGenderDTO);
            context.Add(gender);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] CreateGenderDTO createGenderDTO)
        {
            var gender = await context.Gender.FirstOrDefaultAsync(x => x.Id == Id);

            if (gender == null)
            {
                return NotFound();
            }

            gender = mapper.Map(createGenderDTO, gender);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var exist = await context.Gender.AnyAsync(x => x.Id == Id);

            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Gender() { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
