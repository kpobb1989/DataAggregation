
using Sample.Abstractions.DAL.Interfaces;
using Sample.DAL.Entities;

namespace Sample.Function.Extensions
{
    public static class UnitOfWorkExtensions
    {
        public static IRepository<Package> Packages(this IUnitOfWork unitOfWork) => unitOfWork.GetRepository<Package>();
        public static IRepository<License> Licenses(this IUnitOfWork unitOfWork) => unitOfWork.GetRepository<License>();
    }
}
