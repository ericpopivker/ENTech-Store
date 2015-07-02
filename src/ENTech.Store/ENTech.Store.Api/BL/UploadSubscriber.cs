using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ENTech.Store.Api.BL
{
    public class UploadSubscriber
    {
        public static void SubscribeAllHandlers(UploadEventDispatcher dispatcher)
        {
            dispatcher.Subscribe("Product", () => new ProductFacade());
            //dispatcher.Subscribe("Event", () => new EventFacade());
        }
    }
}