using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal.Utilities
{
    internal class ObjectPool<T>
    {




        public T Rent()
        {
            throw new NotImplementedException();
        }


        public T Rent(Action<T> configure)
        {
            throw new NotImplementedException();
        }


        public void Return(T item)
        {
            throw new NotImplementedException();
        }
    }
}
