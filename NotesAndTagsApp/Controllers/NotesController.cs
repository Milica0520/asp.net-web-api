using Microsoft.AspNetCore.Mvc;
using NotesAndTagsApp.Database;
using NotesAndTagsApp.DTOs;
using NotesAndTagsApp.Models;

namespace NotesAndTagsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {


        [HttpGet("getAll")]
        public ActionResult<List<NoteDto>> Get()
        {
            var notesDto = StaticDb.Notes.Select(//iz jednog tipa izvlaci neke podatke
                n => new NoteDto()
                {
                    Text = n.Text,
                    Priority = (Enums.PriorityEnum)n.Priority,
                    User = n.User.FirstName + n.User.LastName,
                    Tags = n.Tags.Select(t => t.Name).ToList(),

                }).ToList();

            return Ok(notesDto);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetNoteById(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var entity = StaticDb.Notes.Where(n => n.Id == id).Select(n => new NoteDto()
            {
                Text = n.Text,
                Priority = (Enums.PriorityEnum)n.Priority,
                User = n.User.FirstName + n.User.LastName,
                Tags = n.Tags.Select(t => t.Name).ToList(),

            });
            if(entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet("findById/{id}")] //http://localhost:[port]/api/notes/findById?id=2
        public ActionResult<NoteDto> FindById(int id) //id is a query string param
        {

            
            var entity = StaticDb.Notes.Where(n => n.Id == id).Select(n => new NoteDto()
            {
                Text = n.Text,
                Priority = (Enums.PriorityEnum)n.Priority,
                User = n.User.FirstName + n.User.LastName,
                Tags = n.Tags.Select(t => t.Name).ToList(),

            });

            if (entity == null)
            {
                return NotFound();
            }


            return Ok(entity);
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateNoteDto updateNoteDto)
        {



            return Ok(updateNoteDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("user/{userId}")] //route params
        public ActionResult<List<NoteDto>> GetNotesByUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("addNote")]
        public IActionResult AddNote([FromBody] AddNoteDto addNoteDto)
        {
            throw new NotImplementedException();
        }
    }
}
