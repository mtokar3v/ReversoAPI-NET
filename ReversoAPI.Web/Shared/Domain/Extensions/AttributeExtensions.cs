using System;
using System.Linq;

namespace ReversoAPI.Web.Shared.Domain.Extensions
{
    public static class AttributeExtensions
    {
        public static TResult GetAttribute<TResult, TValue>(this TValue value) where TResult : Attribute
        {
            return value.GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(TResult), false)
               .SingleOrDefault() as TResult;
        }
    }
}
