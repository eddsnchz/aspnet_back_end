using System;
using System.Collections.Generic;
using AutoMapper;
using BACKEND.DTOs;
using BACKEND.Entities;
using NetTopologySuite.Geometries;

namespace BACKEND.Utilities
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<CreateGenderDTO, Gender>();
            CreateMap<Actor, MovieActorDTO>().ReverseMap();
            CreateMap<CreateActorDTO, Actor>()
                .ForMember(x => x.Photo, options => options.Ignore());
            CreateMap<CreateCinemaDTO, Cinema>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                 geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
            CreateMap<Cinema, CinemaDTO>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(field => field.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(field => field.Location.X));

            CreateMap<CreateMovieDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MovieActor, options => options.MapFrom(MapMovieGender))
                .ForMember(x => x.MovieCinema, options => options.MapFrom(MapMovieCinema))
                .ForMember(x => x.MovieActor, options => options.MapFrom(MapMovieActor));



        }

        private List<MovieGender> MapMovieGender(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MovieGender>();

            if(createMovieDTO.GendersIds == null)
            {
                return result;
            }

            foreach(var id in createMovieDTO.GendersIds)
            {
                result.Add(new MovieGender() { GenderId = id });
            }

            return result;
        }

        private List<MovieCinema> MapMovieCinema(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MovieCinema>();

            if (createMovieDTO.CinemasIds == null)
            {
                return result;
            }

            foreach (var id in createMovieDTO.CinemasIds)
            {
                result.Add(new MovieCinema() { CinemaId = id });
            }

            return result;
        }

        private List<MovieActor> MapMovieActor(CreateMovieDTO createMovieDTO, Movie movie)
        {
            var result = new List<MovieActor>();

            if (createMovieDTO.Actors == null)
            {
                return result;
            }

            foreach (var actor in createMovieDTO.Actors)
            {
                result.Add(new MovieActor() { ActorId = actor.Id, Character = actor.Character});
            }

            return result;
        }
    }
}
