using System;
using HttpVerbs.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HttpVerbs.Controllers
{
    public class TokensController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            var token = new Random().Next();

            AppStore.Store.Add(token, new Store());

            return Content(token.ToString());
        }
    }
}
