using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SongKeeper.Dto;
using SongKeeper.Interfaces;
using SongKeeper.Models;

namespace SongKeeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IMapper _mapper;
        public TrackController(ITrackRepository trackRepository, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Track>))]
        public IActionResult GetTracks()
        {
            var tracks = _mapper.Map<List<TrackDto>>(_trackRepository.GetTracks());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tracks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Track>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrack(int id)
        {
            var songs = _mapper.Map<TrackDto>(_trackRepository.GetTrack(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(songs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrack([FromBody] TrackDto songCreate)
        {
            if (songCreate == null)
            {
                return BadRequest(ModelState);
            }

            var song = _trackRepository.GetTracks().Where(o => o.Title.Trim().ToUpper() == songCreate.Title.Trim().ToUpper()).FirstOrDefault();

            if (song != null)
            {
                ModelState.AddModelError("", "Track already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var songMap = _mapper.Map<Track>(songCreate);

            if (!_trackRepository.CreateTrack(songMap))
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
        public IActionResult UpdateTrack(int id, [FromBody] TrackDto updatedTrack)
        {
            if (updatedTrack == null)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedTrack.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_trackRepository.TrackExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return BadRequest();

            var songMap = _mapper.Map<Track>(updatedTrack);

            if (!_trackRepository.UpdateTrack(songMap))
            {
                ModelState.AddModelError("", "Something went wrong updating track");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("id")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrack(int id)
        {
            if (!_trackRepository.TrackExists(id))
            {
                return NotFound();
            }

            var trackToDelete = _trackRepository.GetTrack(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_trackRepository.DeleteTrack(trackToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting track");
            }

            return NoContent();
        }
    }
}
