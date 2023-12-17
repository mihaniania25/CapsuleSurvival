using System;
using System.Collections.Generic;

namespace CapsuleSurvival.Utility
{
    [Serializable]
    public class PropagationList<T> : PropagationField<List<T>>
    {
        public PropagationList()
        {
            Value = new List<T>();
        }

        public void Add(T value, bool propagate = true)
        {
            GetValue().Add(value);

            if (propagate)
                Propagate();
        }

        public void Remove(T value, bool propagate = true)
        {
            GetValue().Remove(value);

            if (propagate)
                Propagate();
        }

        public void Clear(bool propagate = true)
        {
            GetValue().Clear();

            if (propagate)
                Propagate();
        }
    }
}