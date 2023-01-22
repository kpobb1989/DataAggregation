using System.ComponentModel.DataAnnotations;

namespace Sample.DAL.Entities
{
    public class PackageToLicense
    {
        public string PackageName { get; set; }
        public string PackageSource { get; set; }
        public string PackageVersion { get; set; }
        public string LicenseName { get; set; }
        public Package Package { get; set; }
        public License License { get; set; }
    }
}
