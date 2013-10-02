using System;
using System.Collections;
using System.Collections.Generic;

namespace Project.Web.Base.Utility
{
    public class TypeMergerPolicy
    {
        private IList ignored;

        public IList Ignored
        {
            get { return this.ignored; }
        }

        public TypeMergerPolicy(IList ignored)
        {
            this.ignored = ignored;
        }

        public TypeMergerPolicy(object ignoreValue)
        {
            ignored = new List<object>();
            ignored.Add(ignoreValue);
        }

        public TypeMergerPolicy Ignore(object value)
        {
            this.ignored.Add(value);
            return this;
        }

        public Object MergeTypes(Object values1, Object values2)
        {
            return TypeMerger.MergeTypes(values1, values2, this);
        }
    }
}
