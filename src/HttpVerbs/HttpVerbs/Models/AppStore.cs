using System;
using System.Collections.Generic;

namespace HttpVerbs.Models
{
    public class AppStore
    {
        public static Dictionary<long, Store> Store = new Dictionary<long, Store>();

        static AppStore()
        {
            Store.Add(0, new Store());
        }
    }

    public class Store
    {
        public List<Post> Posts { get; set; }
        public List<User> Users { get; set; }

        public Store()
        {
            Posts = new List<Post> {
                new Post {
                    PostId = 12, AuthorId = 101,
                    Title = "Do you like cooking?",
                    Body = "The two things that I love most is my family and my food.For me,a happy meal is the key to success,a good food always uplift your mood and concretes your thought process. I am a big time foodie and I absolutely love cooking.Being raised in a bengali family,I have been always bombarded with lip smacking aroma of different dishes,be it biryani,fish curry,chicken curry,moong dal,aloo posto,mix vegetables,pudding,poor and bhaji( luchi aloo dum) and what not.",
                    Created = new DateTime(2018, 01, 18, 12, 20, 20),
                    Tags = new List<string> { "cooking", "food", "love2eat" }
                },
                new Post {
                    PostId = 27, AuthorId = 505,
                    Title = "Dungeons & Dragons: A geeks only game?",
                    Body = "Is it for geeks? I don't know... But, I know for sure one thing. IT IS FUN. Yes, Dungeons and Dragons is fun as it promotes creativity and enjoying the spending of time with one's friends. Dungeons and Dragons invites its players to improvise their actions and think abstractly, which many view as being fun. In addition, it promotes the spending of time with one's friends, and most people consider that to be a fun activity.",
                    Created = new DateTime(2018, 03, 02, 15, 44, 10),
                    Tags = new List<string> { "d&d", "games", "fantasy" }
                },
                new Post {
                    PostId = 33, AuthorId = 33,
                    Title = "Which musical instrument do you love?",
                    Body = "Between the piano and the cello. The piano for its orchestral properties, and the cello for its extreme lyricism and its softer and more masculine tone than the violin. My favorite instrumental ensemble is the string quartet.",
                    Created = new DateTime(2017, 02, 22, 03, 18, 20),
                    Tags = new List<string> { "music", "instruments" }
                },
                new Post {
                    PostId = 44, AuthorId = 228,
                    Title = "Why do you like sports?",
                    Body = "I like sports for the same reason that Benjamin Sisko did in Star Trek Deep Space Nine - no-one knows the outcome of any part of the process prior to the start of it. Not only each game, but each play, is unpredictable. Now, you may watch a movie or TV show and not know what’s going to happen, but someone does.",
                    Created = new DateTime(2016, 01, 01, 12, 20, 20),
                    Tags = new List<string> { "sports", "fit", "healthy", "athlete" }
                },
                new Post {
                    PostId = 58, AuthorId = 103,
                    Title = "Your favorite movie and why?",
                    Body = "This film is \"like a box of chocolates… you never know what you're gonna get,\" as it serves up an abundance of surprising treats, sometimes sweet and sometimes bitter. See what love looks like in its purest form. When you have a kind and gentle heart, happiness learns from you how to be happy!",
                    Created = new DateTime(2017, 02, 03, 12, 20, 20),
                    Tags = new List<string> { "movie", "film", "cinema" }
                },
                new Post {
                    PostId = 59, AuthorId = 103,
                    Title = "Your favorite pet?",
                    Body = "We have A LOT of pets! I couldn’t really say which is my favorite, but since not all of them have names, I’ll tell you the ones who have names. We have Gamble, our Giant Schanauzer; Itchy, a Mastiff mix; Lil Bit, a Pomeranian; Tabu, another Pom; our favorite dwarf goat is Shadow; our favorite hog is named Pig; our favorite fish are a Jack Dempsey and an Oscar which are inseperable, so we call them Jack and Jill; we have a Blue Iquana named Azul; an Ornate Pacman frog named DeGrasse; a cat named Tom. Not very imaginative names, I guess. Sorry.",
                    Created = new DateTime(2017, 02, 03, 14, 20, 20),
                    Tags = new List<string> { "pets" }
                }
            };

            Users = new List<User> {
                new User { Email = "john.gallo@hotmail.com", Name = "John Gallaway", UserId = 101 },
                new User { Email = "spirit88@gmail.com", Name = "George Astor", UserId = 505 },
                new User { Email = "tanamabase@gmail.com", Name = "Samantha Yale", UserId = 33 },
                new User { Email = "irene3birdfly@hotmail.com", Name = "Irene Scoup", UserId = 228 },
                new User { Email = "jackthebottle@gmail.com", Name = "Jack Daniels", UserId = 103 }
            };
        }
    }
}
