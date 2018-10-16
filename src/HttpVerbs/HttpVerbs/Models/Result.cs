namespace HttpVerbs.Models
{
    internal class Result
    {
        public Result(int status)
        {
            Status = status;
        }

        public Result(int status, string text)
        {
            Text = text;
            Status = status;
        }

        public int Status { get; set; }

        public string Text { get; set; }
    }
}
