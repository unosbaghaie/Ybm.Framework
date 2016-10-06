using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Engine
{
    public class EventHelper
    {
        public static EventHandler GetEventHandler(object classInstance, string eventName)
        {
            Type classType = classInstance.GetType();
            FieldInfo eventField = classType.GetField(eventName, BindingFlags.GetField
                                                               | BindingFlags.NonPublic
                                                               | BindingFlags.Instance);

            EventHandler eventDelegate = (EventHandler)eventField.GetValue(classInstance);

            // eventDelegate will be null if no listeners are attached to the event
            if (eventDelegate == null)
                return null;

            return eventDelegate;
        }
    }
}
