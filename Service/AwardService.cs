public class AwardService
{
    private readonly AppDbContext _context;

    public AwardService(AppDbContext context)
    {
        _context = context;
    }

    public object GetAwardIntervals()
    {
        var producersWithWins = _context.Producers
            .Select(p => new
            {
                Producer = p.Name,
                Wins = p.MovieProducers
                    .Where(mp => mp.Movie.IsWinner) // Filtra apenas filmes vencedores
                    .Select(mp => mp.Movie.Year)
                    .OrderBy(year => year)
                    .ToList()
            })
            .Where(p => p.Wins.Count > 1) // Apenas produtores com mais de uma vitória
            .ToList();

        if (!producersWithWins.Any())
        {
            return new { Min = new List<object>(), Max = new List<object>() };
        }

        var intervals = producersWithWins
            .Select(p => new
            {
                p.Producer,
                Intervals = p.Wins.Zip(p.Wins.Skip(1), (prev, next) => new
                {
                    Interval = next - prev,
                    PreviousWin = prev,
                    FollowingWin = next
                })
            });

        var minInterval = intervals
            .SelectMany(p => p.Intervals)
            .Min(i => i.Interval);

        var maxInterval = intervals
            .SelectMany(p => p.Intervals)
            .Max(i => i.Interval);

        return new
        {
            Min = intervals
                .SelectMany(p => p.Intervals
                    .Where(i => i.Interval == minInterval)
                    .Select(i => new
                    {
                        Producer = p.Producer,
                        i.Interval,
                        i.PreviousWin,
                        i.FollowingWin
                    }))
                .ToList(),
            Max = intervals
                .SelectMany(p => p.Intervals
                    .Where(i => i.Interval == maxInterval)
                    .Select(i => new
                    {
                        Producer = p.Producer,
                        i.Interval,
                        i.PreviousWin,
                        i.FollowingWin
                    }))
                .ToList()
        };
    }
}
