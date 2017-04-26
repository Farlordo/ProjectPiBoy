using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPiBoy.Common.Utilities
{
    public static class ReflectionUtil
    {
        /// <summary>
        /// Gets a <see cref="PropertyInfo"/> for a property specified by a lambda expression
        /// </summary>
        /// <typeparam name="TSource">The type of the object</typeparam>
        /// <typeparam name="TProperty">The type of the property</typeparam>
        /// <param name="propertyLambda">A lambda expression that gets the property from the object</param>
        /// <returns>The <see cref="PropertyInfo"/> for the specified property</returns>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression
                ?? throw new ArgumentException($"Expression \"{propertyLambda}\" refers to a method, not a property.");

            PropertyInfo propInfo = member.Member as PropertyInfo
                ?? throw new ArgumentException($"Expression \"{propertyLambda}\" refers to a field, not a property.");

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expression \"{propertyLambda}\" refers to a property that is not from type {type}");

            return propInfo;
        }
    }
}
