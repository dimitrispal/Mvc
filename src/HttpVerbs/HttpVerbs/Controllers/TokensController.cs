namespace HttpVerbs.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using HttpVerbs.Models;

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
