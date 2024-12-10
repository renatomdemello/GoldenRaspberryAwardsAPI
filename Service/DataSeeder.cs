using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Runtime.CompilerServices;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public void Seed(string filePath)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";", // Define o delimitador como ponto e vírgula
            HeaderValidated = null, // Ignora validação de cabeçalhos ausentes
            MissingFieldFound = null // Ignora validação de campos ausentes
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, csvConfig);

        // Registra o mapa para MovieCsvRecord
        csv.Context.RegisterClassMap<MovieCsvRecordMap>();

        var records = csv.GetRecords<MovieCsvRecord>();

        foreach (var record in records)
        {
            var movie = new Movie
            {
                Title = record.Title,
                Year = record.Year,
                IsWinner = record.Winner?.ToLower() == "yes" // Converte "yes" para booleano
            };

            var producers = record.Producers.Split(", ")
                .Select(name => _context.Producers.FirstOrDefault(p => p.Name == name) ?? new Producer { Name = name })
                .ToList();

            Console.WriteLine($"Title: {record.Title}, Year: {record.Year}, Producers: {record.Producers}, Winner: {record.Winner}");

            var winners = _context.Movies.Where(m => m.IsWinner).ToList();
            Console.WriteLine($"Total Winners: {winners.Count}");

            foreach (var producer in producers)
            {
                if (!_context.Producers.Contains(producer))
                    _context.Producers.Add(producer);

                _context.MovieProducers.Add(new MovieProducer
                {
                    Movie = movie,
                    Producer = producer
                });
            }

            _context.Movies.Add(movie);
        }

        _context.SaveChanges();
    }

}

public class MovieCsvRecord
{
    public int Year { get; set; }
    public string Title { get; set; }
    public string Studios { get; set; }
    public string Producers { get; set; }
    public string Winner { get; set; }
}

public class MovieCsvRecordMap : ClassMap<MovieCsvRecord>
{
    public MovieCsvRecordMap()
    {
        Map(m => m.Year).Name("year");
        Map(m => m.Title).Name("title");
        Map(m => m.Studios).Name("studios");
        Map(m => m.Producers).Name("producers");
        Map(m => m.Winner).Name("winner");
    }
}
