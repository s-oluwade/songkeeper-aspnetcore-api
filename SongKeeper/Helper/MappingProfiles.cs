﻿using AutoMapper;
using SongKeeper.Dto;
using SongKeeper.Models;

namespace SongKeeper.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<AlbumDto, Album>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDto, Artist>();
            CreateMap<Track, TrackDto>();
            CreateMap<Track, Track>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();
            CreateMap<Favorite, FavoriteDto>();
            CreateMap<FavoriteDto, Favorite>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
