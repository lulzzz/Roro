﻿
using System;
using System.Drawing;
using System.Linq;

namespace Roro
{
    public abstract class Element
    {
        public abstract string Path { get; }

        public abstract Rectangle Bounds { get; }

        public ElementQuery GetQuery()
        {
            var query = new ElementQuery();
            var props = this.GetType().GetProperties().Where(attr => Attribute.IsDefined(attr, typeof(Property)));
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttributes(typeof(Property), false).First() as Property;
                var condition = new Condition(prop.Name, prop.GetValue(this), attr.Enabled, attr.Required);
                query.Add(condition);
            }
            return query;
        }

        public bool TryQuery(ElementQuery query)
        {
            foreach (var condition in query)
            {
                if (condition.Required || condition.Enabled)
                {
                    var prop = this.GetType().GetProperty(condition.Name);
                    if (condition.Compare(prop.GetValue(this), prop.PropertyType))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
    }
}
