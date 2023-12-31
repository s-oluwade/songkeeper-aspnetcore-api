using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SongKeeper.Dto;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;
        public ArtistController(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
        public IActionResult GetArtists()
        {
            var artists = _mapper.Map<List<ArtistDto>>(_artistRepository.GetArtists());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(artists);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Artist))]
        [ProducesResponseType(400)]
        public IActionResult GetArtist(int id)
        {
            var artist = _mapper.Map<ArtistDto>(_artistRepository.GetArtist(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(artist);
        }

        [HttpGet("{id}/albums")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
        [ProducesResponseType(400)]
        public IActionResult GetArtistAlbums(int id)
        {
            var albums = _mapper.Map<AlbumDto>(_artistRepository.GetAlbums(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(albums);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateArtist([FromBody] ArtistDto artistCreate)
        {
            if (artistCreate == null)
            {
                return BadRequest(ModelState);
            }

            var artist = _artistRepository.GetArtists().Where(o => o.Name.Trim().ToUpper() == artistCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (artist != null)
            {
                ModelState.AddModelError("", "Artist already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var artistMap = _mapper.Map<Artist>(artistCreate);

            if (!_artistRepository.CreateArtist(artistMap))
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
        public IActionResult UpdateArtist(int id, [FromBody] ArtistDto updatedArtist)
        {
            if (updatedArtist == null)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedArtist.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_artistRepository.ArtistExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var artistMap = _mapper.Map<Artist>(updatedArtist);

            if (!_artistRepository.UpdateArtist(artistMap))
            {
                ModelState.AddModelError("", "Something went wrong updating artist");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteArtist(int id)
        {
            if (!_artistRepository.ArtistExists(id))
            {
                return NotFound();
            }

            var artistToDelete = _artistRepository.GetArtist(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_artistRepository.DeleteArtist(artistToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting artist");
            }

            return NoContent();
        }
    }
}
