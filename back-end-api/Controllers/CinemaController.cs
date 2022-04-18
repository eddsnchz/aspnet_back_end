using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Entities;
using BACKEND.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController:ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CinemaController(ApplicationDBContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CinemaDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Cinema.AsQueryable();
            await HttpContext.InsertParamsPaginationInHeader(queryable);
            var cinemas = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<CinemaDTO>>(cinemas);
        }


        [HttpGet("{Id:int}")] //api/cinema/1
        public async Task<ActionResult<CinemaDTO>> GetById(int Id, [FromHeader] string ValueDefault)
        {
            var cinema = await context.Cinema.FirstOrDefaultAsync(x => x.Id == Id);

            if (cinema == null)
            {
                return NotFound();
            }

            return mapper.Map<CinemaDTO>(cinema);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCinemaDTO createCinemaDTO)
        {
            var cinema = mapper.Map<Cinema>(createCinemaDTO);
            context.Add(cinema);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] CreateCinemaDTO createCinemaDTO)
        {
            var cinema = await context.Cinema.FirstOrDefaultAsync(x => x.Id == Id);

            if (cinema == null)
            {
                return NotFound();
            }

            cinema = mapper.Map(createCinemaDTO, cinema);

            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var exist = await context.Cinema.AnyAsync(x => x.Id == Id);

            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Cinema() { Id = Id });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
