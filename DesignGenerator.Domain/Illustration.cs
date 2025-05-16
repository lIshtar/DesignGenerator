namespace DesignGenerator.Domain
{
    public class Illustration
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Prompt { get; set; }
        public string IllustrationPath { get; set; }
        public bool IsReviewed { get; set; }
        public DateTime GenerationDate { get; set; } = DateTime.Now;
    }
}
