using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ViewSamples.Models
{
    public static class MemoryContext //: DbContext
    {
        private const string DbName = "db.json";

        public static ICollection<Movie> Movies { get; set; }

        static MemoryContext()
        {
            if (!File.Exists(DbName)) {
                Movies = new List<Movie>();
            } else {
                var str = File.ReadAllText(DbName);
                Movies = JsonConvert.DeserializeObject<List<Movie>>(str);
            }
        }

        public static bool SaveChanges()
        {
            try {
                var str = JsonConvert.SerializeObject(Movies);
                File.WriteAllText(DbName, str);

                return true;
            } catch {
                return false;
            }
        }
    }
}
