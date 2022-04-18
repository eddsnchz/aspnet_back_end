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
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string container = "actors";

        public ActorController(ApplicationDBContext context, IMapper mapper, IFileStorage fileStorage)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Actor.AsQueryable();
            await HttpContext.InsertParamsPaginationInHeader(queryable);
            var actors = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return mapper.Map<List<ActorDTO>>(actors);
        }

        [HttpPost("searchByName")]
        public async Task<ActionResult<List<MovieActorDTO>>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<MovieActorDTO>();
            }

            return await context.Actor
                .Where(x => x.Name.Contains(name))
                .Select(x => new MovieActorDTO { Id = x.Id, Name = x.Name, Photo = x.Photo })
                .Take(5)
                .ToListAsync();
        }

        [HttpGet("{Id}")] //api/actor/1
        public async Task<ActionResult<ActorDTO>> GetById(int Id, [FromHeader] string ValueDefault)
        {
            var actor = await context.Actor.FirstOrDefaultAsync(x => x.Id == Id);

            if (actor == null)
            {
                return NotFound();
            }

            return mapper.Map<ActorDTO>(actor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateActorDTO createActorDTO)
        {
            var actor = mapper.Map<Actor>(createActorDTO);

            if (createActorDTO.Photo != null)
                actor.Photo = await fileStorage.SaveFile(container, createActorDTO.Photo);

            context.Add(actor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Put(int Id, [FromForm] CreateActorDTO createActorDTO)
        {
            var actor = await context.Actor.FirstOrDefaultAsync(x => x.Id == Id);

            if (actor == null)
                return NotFound();

            actor = mapper.Map(createActorDTO, actor);

            if (createActorDTO.Photo != null)
                actor.Photo = await fileStorage.EditFile(container, createActorDTO.Photo, actor.Photo);

            await context.SaveChangesAsync();
            return NoContent();

        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var actor = await context.Actor.FirstOrDefaultAsync(x => x.Id == Id);

            if (actor == null)
            {
                return NotFound();
            }

            context.Remove(new Actor() { Id = Id });
            await context.SaveChangesAsync();

            await fileStorage.DeleteFile(actor.Photo, container);
            return NoContent();
        }
    }
}
