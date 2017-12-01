using System.Data.Linq.Mapping;

namespace ClubInfo
{
    [Table(Name = "TestUserTable")]
    public class ClubVisitor
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int PersonID { get; set; }

        [Column(Name = "PersonName")]
        public string personName { get; set; }

        [Column(Name = "PersonSkill")]
        public string personSkill { get; set; }
    }
}
