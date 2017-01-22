using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    [Serializable]
    internal sealed class SingletonSerializationHelper : IObjectReference
    {
        // GetRealObject is called after this object is deserialized.
        public object GetRealObject(StreamingContext context)
        {
            // When deserialiing this object, return a reference to 
            // the Singleton object instead.
            return RestaurantPlan.Instance;
        }
    }
}
