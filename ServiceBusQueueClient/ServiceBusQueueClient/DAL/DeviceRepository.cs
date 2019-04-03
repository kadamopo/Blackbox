using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ServiceBusQueueClient.Models;

namespace ServiceBusQueueClient.DAL
{
    public class DeviceRepository : IDeviceRepository
    {
        public IRepository<Device> Repository { get; private set; }

        public DeviceRepository(DbContext dbContext)
        {
            Repository = new Repository<Device>(dbContext);
        }

        public DeviceRepository(IRepository<Device> repository)
        {
            Repository = repository;
        }

        public IQueryable<Device> GetAll()
        {
            return Repository.GetAll();
        }

        public Device GetById(string id)
        {
            return Repository.GetById(id);
        }

        public IQueryable<Device> SearchFor(Expression<Func<Device, bool>> predicate)
        {
            return Repository.SearchFor(predicate);
        }

        public void Update(Device device)
        {
            Repository.Update(device);
        }

        public void Insert(Device device)
        {
            Repository.Insert(device);
        }

        public void Delete(Device device)
        {
            Repository.Delete(device);
        }
    }
}
