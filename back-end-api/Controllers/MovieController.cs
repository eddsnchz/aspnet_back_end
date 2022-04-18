using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Entities;
using BACKEND.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController: ControllerBase
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly IFileStorage fileStorage;
        private readonly string container = "movies";

        public MovieController(ApplicationDBContext context, IMapper mapper, IFileStorage fileStorage)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorage = fileStorage;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<MoviesPostGetDTO>> PostGet()
        {
            var cinemas = await context.Cinema.ToListAsync();
            var genders = await context.Gender.ToListAsync();

            var cinemasDTO = mapper.Map<List<CinemaDTO>>(cinemas);
            var gendersDTO = mapper.Map<List<GenderDTO>>(genders);

            return new MoviesPostGetDTO() { Cinema = cinemasDTO, Gender = gendersDTO };

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateMovieDTO createMovieDTO)
        {
            var movie = mapper.Map<Movie>(createMovieDTO);

            if (createMovieDTO.Poster != null)
                movie.Poster = await fileStorage.SaveFile(container, createMovieDTO.Poster);

            WriteActorOrder(movie);
            context.Add(movie);
            await context.SaveChangesAsync();
            return NoContent();

        }

        private void WriteActorOrder(Movie movie)
        {
            if(movie.MovieActor != null)
            {
                for(int i=0; i <movie.MovieActor.Count; i++)
                {
                    movie.MovieActor[i].Order = i;
                }
            }
        }
    }
}
