namespace HttpVerbs.Controllers
{
    using System;
    using System.Linq;

    using HttpVerbs.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("posts")]
    public class PostsController : Controller
    {
        private Store store_;

        [HttpGet("{id?}")]
        public ActionResult Index(int? id, int? authorId)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var posts = store_.Posts;

            if (id != null) {
                var result = posts.SingleOrDefault(p => p.PostId == id);

                return result == null
                    ? (ActionResult)NotFound()
                    : (ActionResult)Json(result);
            }

            if (authorId != null) {
                posts = posts.Where(p => p.AuthorId == authorId).ToList();
            }
            
            return Json(posts);
        }

        [HttpGet("{id}/tags")]
        public ActionResult Tags(int id)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var post = store_.Posts.SingleOrDefault(p => p.PostId == id);

            if (post == null) {
                return NotFound();
            }

            return Json(post.Tags);
        }


        [HttpPost]
        public ActionResult Create([FromBody] Post post)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            if (post.PostId != 0) {
                return BadRequest("Cannot specify Post Id manually");
            }

            // generate random id
            post.PostId = new Random().Next();

            var result = ValidatePost(post);

            if (result.Status != StatusCodes.Status200OK) {
                return StatusCode(result.Status, result.Text);
            }

            store_.Posts.Add(post);

            return Created($"/posts/{post.PostId}", post);
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

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Post post)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var dbPost = store_.Posts.SingleOrDefault(p => p.PostId == id);
            
            if (dbPost == null) {
                return NotFound();
            }

            if (post.PostId != 0) {
                return BadRequest("Cannot specify postId in body");
            }

            var result = ValidatePost(post);

            if (result.Status != StatusCodes.Status200OK) {
                return StatusCode(result.Status, result.Text);
            }

            dbPost.Body = post.Body;
            dbPost.Title = post.Title;
            dbPost.Created = post.Created;
            dbPost.AuthorId = post.AuthorId;

            return Ok();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] Post post)
        {
            var token = GetToken();

            if (token == null) {
                return Unauthorized();
            }

            var dbPost = store_.Posts.SingleOrDefault(p => p.PostId == id);
            
            if (dbPost == null) {
                return NotFound();
            }

            if (post.PostId != 0) {
                return BadRequest("Cannot specify postId in body");
            }

            if (post.AuthorId != 0 && post.AuthorId != dbPost.AuthorId) {

                if (!store_.Users.Any(u => u.UserId == post.AuthorId)) {
                    return BadRequest("User not found");
                }

                dbPost.AuthorId = post.AuthorId;
            }

            if (!string.IsNullOrWhiteSpace(post.Title)) {
                dbPost.Title = post.Title;
            }

            if (!string.IsNullOrWhiteSpace(post.Body)) {
                dbPost.Body = post.Body;
            }

            if (post.Created != DateTime.MinValue) {
                dbPost.Created = post.Created;
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

            if (!store_.Posts.Any(p => p.PostId == id)) {
                return NotFound();
            }

            store_.Posts.Remove(store_.Posts.Single(p => p.PostId == id));

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
