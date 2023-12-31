using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SongKeeper.Dto;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
        public IActionResult GetGenres()
        {
            var genres = _mapper.Map<List<GenreDto>>(_genreRepository.GetGenres());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genres);
        }

        [HttpGet("{id}/albums")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
        public IActionResult GetAlbums(int id)
        {
            var genres = _mapper.Map<List<AlbumDto>>(_genreRepository.GetAlbums(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genres);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Genre))]
        [ProducesResponseType(400)]
        public IActionResult GetGenre(int id)
        {
            var genre = _mapper.Map<GenreDto>(_genreRepository.GetGenre(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGenre([FromBody] GenreDto genreCreate)
        {
            if (genreCreate == null)
            {
                return BadRequest(ModelState);
            }

            var genre = _genreRepository.GetGenres().Where(o => o.Name.Trim().ToUpper() == genreCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (genre != null)
            {
                ModelState.AddModelError("", "Genre already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genreMap = _mapper.Map<Genre>(genreCreate);

            if (!_genreRepository.CreateGenre(genreMap))
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
        public IActionResult UpdateGenre(int id, [FromBody] GenreDto updatedGenre)
        {
            if (updatedGenre == null)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedGenre.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_genreRepository.GenreExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var genreMap = _mapper.Map<Genre>(updatedGenre);

            if (!_genreRepository.UpdateGenre(genreMap))
            {
                ModelState.AddModelError("", "Something went wrong updating genre");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteGenre(int id)
        {
            if (!_genreRepository.GenreExists(id))
            {
                return NotFound();
            }

            var genreToDelete = _genreRepository.GetGenre(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_genreRepository.DeleteGenre(genreToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting genre");
            }

            return NoContent();
        }
    }
}
