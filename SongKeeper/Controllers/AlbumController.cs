
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SongKeeper.Dto;
using SongKeeper.Interfaces;
using SongKeeper.Models;
using System.Security.AccessControl;

namespace SongKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;
        public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
        public IActionResult GetAlbums()
        {
            var albums = _mapper.Map<List<AlbumDto>>(_albumRepository.GetAlbums());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(albums);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Album))]
        [ProducesResponseType(400)]
        public IActionResult GetAlbum(int id)
        {
            var album = _mapper.Map<AlbumDto>(_albumRepository.GetAlbum(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(album);
        }

        [HttpGet("{id}/tracks")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Track>))]
        [ProducesResponseType(400)]
        public IActionResult GetTracks(int id)
        {
            var tracks = _mapper.Map<TrackDto>(_albumRepository.GetTracks(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tracks);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAlbum([FromBody] AlbumDto albumCreate)
        {
            if (albumCreate == null)
            {
                return BadRequest(ModelState);
            }

            var album = _albumRepository.GetAlbums().Where(o => o.Title.Trim().ToUpper() == albumCreate.Title.Trim().ToUpper()).FirstOrDefault();

            if (album != null)
            {
                ModelState.AddModelError("", "Album already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var albumMap = _mapper.Map<Album>(albumCreate);

            if (!_albumRepository.CreateAlbum(albumMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAlbum(int id, [FromBody] AlbumDto updatedAlbum)
        {
            if (updatedAlbum == null)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedAlbum.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_albumRepository.AlbumExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var albumMap = _mapper.Map<Album>(updatedAlbum);

            if (!_albumRepository.UpdateAlbum(albumMap))
            {
                ModelState.AddModelError("", "Something went wrong updating album");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAlbum(int id)
        {
            if (!_albumRepository.AlbumExists(id))
            {
                return NotFound();
            }

            var albumToDelete = _albumRepository.GetAlbum(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_albumRepository.DeleteAlbum(albumToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting album");
            }

            return NoContent();
        }
    }
}
