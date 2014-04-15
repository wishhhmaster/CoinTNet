using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CoinTNet
{
    /// <summary>
    /// Context for the application
    /// </summary>
    public class CoinTNetContext : DbContext
    {
        public DbSet<NewsSource> NewsSources { get; set; }
    }

    /// <summary>
    /// Represents a news source
    /// </summary>
    [Table("NewsSource")]
    public class NewsSource
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public int NbItems { get; set; }
        public string Name { get; set; }
        public string Filter { get; set; }
        public int ExpiryInHours { get; set; }
    }
}
