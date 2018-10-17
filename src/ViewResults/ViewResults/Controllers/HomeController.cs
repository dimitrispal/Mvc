using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ViewResults.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // no view name - defaults to Index.cshtml
        }

        public IActionResult IndexView()
        {
            return View("Privacy"); // view name specified
        }

        public IActionResult Content()
        {
            return Content(
                "\ud83d\ude1d\ud83c\udfa8\ud83c\udf83\ud83c\udf3b\ud83c\udfa8\ud83c\udfb8\ud83c\udf3b\ud83c\udfb8", // content 
                MediaTypeNames.Text.Plain, // media type (optional)
                System.Text.Encoding.UTF8); // encoding (optional)
        }

        public IActionResult Empty()
        {
            return new EmptyResult();
        }

        public IActionResult FileResult()
        {
            // to download, add third parameter with name, e.g "filename.jpg". 
            // This will also change Content-Disposition header from inline to attachemnt.
            return File("/favicon.ico", "image/x-icon");
        }

        [HttpGet("/api/movies")] // overrides default route
        public IActionResult JsonResult()
        {
            return Json(new {
                movies = new[] {
                    new { title = "The Shawshank Redemption", year = 1994, rating = 9.3M, genre = new[] { "Crime", "Drama" } },
                    new { title = "The Dark Knight", year = 2008, rating = 9M, genre = new[] { "Action", "Crime", "Drama" } },
                    new { title = "12 Angry Men", year = 1957, rating = 8.9M, genre = new[] { "Crime", "Drama" } }
                }
            });
        }

        [HttpGet("{method:int?}")]
        public IActionResult RedirectResult(int method)
        {
            switch (method) {
                case 1:
                    return Redirect("https://www.google.com"); // absolute path
                    // return Redirect("2"); for relative path
                case 2:
                    return Redirect(Url.Action("Index", "Home"));
                case 3:
                    return RedirectToActionPermanent("FileResult", "Home"); // action name, controller name
                default:
                    // return BadRequest(); // you could, but consider the following:
                    return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }
}
