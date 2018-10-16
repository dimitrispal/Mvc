using System;
using System.Collections.Generic;

namespace HttpVerbs.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Body { get; set; }

        public string Title { get; set; }

        public int AuthorId { get; set; }

        public DateTime Created { get; set; }

        public List<string> Tags { get; set; }

        public Post()
        {
            Created = DateTime.Now;
        }
    }
}
