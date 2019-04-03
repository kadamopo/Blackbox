using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using ServiceBusQueueClient.Models;

namespace ServiceBusQueueClient.DAL
{
    public class DeviceMessageRepository : IDeviceMessageRepository
    {
        public IRepository<DeviceMessage> Repository { get; private set; }

        public DeviceMessageRepository(DbContext dbContext)
        {
            Repository = new Repository<DeviceMessage>(dbContext);
        }

        public DeviceMessageRepository(IRepository<DeviceMessage> repository)
        {
            Repository = repository;
        }

        public IQueryable<DeviceMessage> GetAll()
        {
            return Repository.GetAll();
        }

        public DeviceMessage GetById(int id)
        {
            return Repository.GetById(id);
        }

        public IQueryable<DeviceMessage> SearchFor(Expression<Func<DeviceMessage, bool>> predicate)
        {
            return Repository.SearchFor(predicate);
        }

        public void Update(DeviceMessage deviceMessage)
        {
            Repository.Update(deviceMessage);
        }

        public void Insert(DeviceMessage deviceMessage)
        {
            Repository.Insert(deviceMessage);
        }

        public void Delete(DeviceMessage deviceMessage)
        {
            Repository.Delete(deviceMessage);
        }
    }

}
