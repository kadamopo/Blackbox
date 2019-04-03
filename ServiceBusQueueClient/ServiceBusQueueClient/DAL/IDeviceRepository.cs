using System;
using System.Linq;
using System.Linq.Expressions;
using ServiceBusQueueClient.Models;

namespace ServiceBusQueueClient.DAL
{
    public interface IDeviceRepository
    {
        IRepository<Device> Repository { get; }

        void Delete(Device device);
        IQueryable<Device> GetAll();
        Device GetById(string id);
        void Insert(Device device);
        IQueryable<Device> SearchFor(Expression<Func<Device, bool>> predicate);
        void Update(Device device);
    }
}