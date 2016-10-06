using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ybm.Framework.Service
{
    public class EntityDeletingEventArgs<T> : EventArgs
    {
        public T SavedEntity;

    }
}
