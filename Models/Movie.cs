public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public bool IsWinner { get; set; }
    public List<MovieProducer> MovieProducers { get; set; }
}
