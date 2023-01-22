using Sample.Abstractions.DAL;

using System.ComponentModel.DataAnnotations;

namespace Sample.DAL.Entities
{
    public class Package : RefreshableDbEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Source { get; set; }

        [StringLength(100)]
        public string Version { get; set; }

        public ICollection<License> Licenses { get; set; } = new List<License>();

        public override bool Equals(DbEntity? other)
        {
            var package = other as Package;

            if (package != null)
            {
                return Name == package.Name &&
                    Source == package.Source &&
                    Version == package.Version &&
                    Licenses.Intersect(package.Licenses).Count() == package.Licenses.Count;
            }

            return Equals((object?)other);
        }
        public override void Update(DbEntity other)
        {
            var package = other as Package;

            if (package != null)
            {
                Name = package.Name;
                Source = package.Source;
                Version = package.Version;
                Licenses = package.Licenses;
                LastUpdate = package.LastUpdate;
            }
        }
    }
}
