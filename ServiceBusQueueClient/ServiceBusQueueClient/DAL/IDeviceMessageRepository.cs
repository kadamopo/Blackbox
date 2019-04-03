using System;
using System.Linq;
using System.Linq.Expressions;
using ServiceBusQueueClient.Models;

namespace ServiceBusQueueClient.DAL
{
    public interface IDeviceMessageRepository
    {
        IRepository<DeviceMessage> Repository { get; }

        void Delete(DeviceMessage deviceMessage);
        IQueryable<DeviceMessage> GetAll();
        DeviceMessage GetById(int id);
        void Insert(DeviceMessage deviceMessage);
        IQueryable<DeviceMessage> SearchFor(Expression<Func<DeviceMessage, bool>> predicate);
        void Update(DeviceMessage deviceMessage);
    }
}