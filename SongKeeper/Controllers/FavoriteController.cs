using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SongKeeper.Dto;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;
        public FavoriteController(IFavoriteRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Favorite>))]
        public IActionResult GetFavorites()
        {
            var favorites = _mapper.Map<List<FavoriteDto>>(_favoriteRepository.GetFavorites());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(favorites);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Favorite))]
        [ProducesResponseType(400)]
        public IActionResult GetFavorite(int id)
        {
            var favorite = _mapper.Map<FavoriteDto>(_favoriteRepository.GetFavorite(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(favorite);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFavorite([FromBody] FavoriteDto favoriteCreate)
        {
            if (favoriteCreate == null)
            {
                return BadRequest(ModelState);
            }

            var favorite = _favoriteRepository.GetFavorites()
                .Where(o => o.Type.Trim().ToUpper() == favoriteCreate.Type.Trim().ToUpper() &&
                            o.UserId == favoriteCreate.UserId).FirstOrDefault();

            if (favorite != null)
            {
                ModelState.AddModelError("", "Favorite already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var favoriteMap = _mapper.Map<Favorite>(favoriteCreate);

            if (!_favoriteRepository.CreateFavorite(favoriteMap))
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
        public IActionResult UpdateFavorite(int id, [FromBody] FavoriteDto updatedFavorite)
        {
            if (updatedFavorite == null)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedFavorite.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_favoriteRepository.FavoriteExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var favoriteMap = _mapper.Map<Favorite>(updatedFavorite);

            if (!_favoriteRepository.UpdateFavorite(favoriteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating favorite");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFavorite(int id)
        {
            if (!_favoriteRepository.FavoriteExists(id))
            {
                return NotFound();
            }

            var favoriteToDelete = _favoriteRepository.GetFavorite(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_favoriteRepository.DeleteFavorite(favoriteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting favorite");
            }

            return NoContent();
        }
    }
}
