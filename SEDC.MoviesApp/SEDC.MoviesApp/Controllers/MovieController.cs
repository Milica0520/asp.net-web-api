using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SEDC.MoviesApp.Database;
using SEDC.MoviesApp.Dtos;
using SEDC.MoviesApp.Enums;
using SEDC.MoviesApp.Models;

namespace SEDC.MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpGet] //api/movies
        public ActionResult<List<MovieDto>> Get()
        {


            try
            {
                List<MovieDto> movieList = StaticDB.Movies.Select(movie => new MovieDto()
                {
                    Title = movie.Title,
                    Year = movie.Year,
                    Genre = movie.Genre,
                    Description = movie.Description,
                }).ToList();

                if (movieList == null)
                {
                    return NotFound("Movies not faund");
                }

                return Ok(movieList);
            }
            catch (Exception message)
            {
                throw message;
            }
        }


        [HttpGet("{id}")] //api/movies/2
        public ActionResult<MovieDto> Get(int id)
        {

            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id can not be negative or zero.");
                }
                var movieById = StaticDB.Movies
                    .Where(movie => movie.Id == id)
                    .Select(m => new MovieDto()
                    {
                        Title = m.Title,
                        Year = m.Year,
                        Genre = m.Genre,
                        Description = m.Description,
                    });
                if (movieById == null)
                {
                    return NotFound("Movie not faund");
                };

                return Ok(movieById);
            }
            catch (Exception message)
            {
                throw message;
            }
        }

        [HttpGet("getById")] //api/movies/queryString?index=1
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("Input can not be negative or zero");
                }

                var movieByQueryId = StaticDB.Movies
                    .Where(movie => movie.Id == id)
                    .Select(m => new MovieDto()
                    {
                        Title = m.Title,
                        Year = m.Year,
                        Genre = m.Genre,
                        Description = m.Description,
                    });

                return Ok(movieByQueryId);
            }
            catch (Exception message)
            {
                throw message;
            }
        }

        [HttpGet("filter")]   //api/movies/filter?genre=1&year=2022  
        public ActionResult<List<MovieDto>> FilterMoviesFromQuery(int? genre, int? year)
        {
            try
            {
                if (!genre.HasValue && !year.HasValue)
                {
                    return BadRequest("Enter data for genre or year");
                }

                List<MovieDto> movies = StaticDB.Movies
                     .Where(movie => ((int)movie.Genre) == genre || movie.Year == year)
                     .Select(m => new MovieDto()
                     {
                         Title = m.Title,
                         Year = m.Year,
                         Genre = m.Genre,
                         Description = m.Description,

                     }).ToList();

                return Ok(movies);
            }
            catch (Exception message)
            {
                throw message;
            }
        }


        [HttpPut("update")]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto movie)
        {
           

            return Ok();
        }


        //[HttpDelete("delete")]
        //public IActionResult DeleteMovie([FromBody] int id)
        //{
        //    return Ok(id);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        // ....
        // Implement AddMovie movie here, AddMovieDto parameter from body it should be http post etc...
        // ....

    }
}
