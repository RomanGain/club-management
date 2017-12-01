using System.Data.Linq.Mapping;

namespace ClubInfo
{
    [Table(Name = "SongsTable")]
    public class ClubSong
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Title")]
        public string songTitle { get; set; }

        [Column(Name = "Style")]
        public string songStyle { get; set; }

    }
}
