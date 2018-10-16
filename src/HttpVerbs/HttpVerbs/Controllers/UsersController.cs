namespace HttpVerbs.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using HttpVerbs.Models;
    using System.Text.RegularExpressions;

    [Route("users")]
    public class UsersController : Controller
    {
        private Store store_;

        [HttpGet("{id?}")]
        public ActionResult Index(int? id)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var users = store_.Users;

            if (id != null) {
                var result = users.SingleOrDefault(u => u.UserId == id);

                return result == null
                    ? (ActionResult)NotFound()
                    : (ActionResult)Json(result);
            }

            return Json(users);
        }

        [HttpPost]
        public ActionResult Create([FromBody] User user)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            if (user.UserId != 0) {
                return BadRequest("Cannot specify User Id manually");
            }

            // generate random id
            user.UserId = new Random().Next();

            var result = ValidateUser(user);

            if (result.Status != StatusCodes.Status200OK) {
                return StatusCode(result.Status, result.Text);
            }

            store_.Users.Add(user);

            return Created($"/users/{user.UserId}", user);
        }

        private Result ValidatePost(Post post)
        {
            if (store_.Posts.Any(p => p.PostId == post.PostId)) {
                return new Result(StatusCodes.Status409Conflict, "Post exists");
            }

            if (string.IsNullOrWhiteSpace(post.Title)) {
                return new Result(StatusCodes.Status400BadRequest, "Title cannot be empty");
            }

            if (!store_.Users.Any(u => u.UserId == post.AuthorId)) {
                return new Result(StatusCodes.Status400BadRequest, "User not found");
            }

            return new Result(StatusCodes.Status200OK);
        }

        private Result ValidateUser(User user)
        {
            if (store_.Users.Any(u => u.UserId == user.UserId)) {
                return new Result(StatusCodes.Status409Conflict, "User exists");
            }

            if (string.IsNullOrWhiteSpace(user.Name)) {
                return new Result(StatusCodes.Status400BadRequest, "Name cannot be empty");
            }

            if (!Regex.IsMatch(user.Email ?? "", @"^\w+@\w+\.\w{1,}$")) {
                return new Result(StatusCodes.Status400BadRequest, "Email must consist only of A-Z, a-z, 0-9 and _");
            }

            return new Result(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] User user)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var dbUser = store_.Users.SingleOrDefault(u => u.UserId == id);
            
            if (dbUser == null) {
                return NotFound();
            }

            if (user.UserId != 0) {
                return BadRequest("Cannot specify userId in body");
            }

            var result = ValidateUser(user);

            if (result.Status != StatusCodes.Status200OK) {
                return StatusCode(result.Status, result.Text);
            }

            dbUser.Email = user.Email;
            dbUser.Name = user.Name;

            return Ok();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] User user)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var dbUser = store_.Users.SingleOrDefault(u => u.UserId == id);
            
            if (dbUser == null) {
                return NotFound();
            }

            if (user.UserId != 0) {
                return BadRequest("Cannot specify userId in body");
            }

            if (!string.IsNullOrWhiteSpace(user.Name)) {
                dbUser.Name = user.Name;
            }

            if (!string.IsNullOrWhiteSpace(user.Email)) {
                if (!Regex.IsMatch(user.Email, @"^\w+@\w+\.\w{1,}$")) {
                    return BadRequest("Email must consist only of A-Z, a-z, 0-9 and _");
                }

                dbUser.Email = user.Email;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var dbUser = store_.Users.SingleOrDefault(u => u.UserId == id);

            if (dbUser == null) {
                return NotFound();
            }

            store_.Users.Remove(dbUser);

            return Ok();
        }

        private int? GetToken()
        {
            var token = Request.Headers["X-Token"];

            if (string.IsNullOrWhiteSpace(token)) {
                return null;
            }

            if (!int.TryParse(token, out var tokenInt)) {
                return null;
            }

            if (!AppStore.Store.ContainsKey(tokenInt)) {
                return null;
            }

            store_ = AppStore.Store[tokenInt];

            return tokenInt;
        }
    }
}
