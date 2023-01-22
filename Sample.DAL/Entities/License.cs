using Sample.Abstractions.DAL;

using System.ComponentModel.DataAnnotations;

namespace Sample.DAL.Entities
{
    public class License : RefreshableDbEntity
    {
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Package> Packages { get; set; } = new List<Package>();

        public override bool Equals(DbEntity? other)
        {
            var license = other as License;

            if (license != null)
            {
                return Name == license?.Name;
            }

            return Equals((object?)other);
        }

        public override void Update(DbEntity other)
        {
            var license = other as License;

            if (license != null)
            {
                Name = license.Name;
                LastUpdate = license.LastUpdate;
            }
        }
    }
}
