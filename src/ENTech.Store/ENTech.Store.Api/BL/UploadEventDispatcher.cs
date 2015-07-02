using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ENTech.Store.Api.DAL;

namespace ENTech.Store.Api.BL
{
    public class UploadEventDispatcher : IUploadEventPublisher
    {
        public delegate IUploadedEventHandler CreateHandlerDelegate();
        private readonly Dictionary<string, CreateHandlerDelegate> _factory = new Dictionary<string, CreateHandlerDelegate>();

        public void Subscribe(string name, CreateHandlerDelegate d)
        {
            _factory.Add(name, d);
        }

        public void Publish(UploadFinishedEvent e)
        {
            IUploadedEventHandler h = FindHandler(e.AttachedEntityType);
            if (h == null)
                throw new Exception("Handler not found");

            h.OnUploaded(e.UploadId, e.AttachedEntityId, e.AttachedEntityFieldName, e.Url);
        }

        private IUploadedEventHandler FindHandler(string entityType)
        {
            if (_factory.ContainsKey(entityType))
                return _factory[entityType]();

            return null;
        }
    }
}