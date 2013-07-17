using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace DemoApp.Core
{
    public class SimpleMapper
    {
        private readonly IDictionary<Tuple<Type, Type>, Delegate> _mappers;

        private Func<T, V> GetMapper<T, V>()
        {
            return (Func<T, V>)_mappers[new Tuple<Type, Type>(typeof(T), typeof(V))];
        }

        public SimpleMapper()
        {
            _mappers = new Dictionary<Tuple<Type, Type>, Delegate>();
        }

        public void AddMapper<T, V>(Func<T, V> mapper)
        {
            _mappers.Add(new Tuple<Type, Type>(typeof(T), typeof(V)), mapper);
        }
        public V Map<T, V>(T aObject)
        {
            var mapper = GetMapper<T, V>();
            if (mapper == null)
                throw new Exception("Mapper method in not found");
            return mapper(aObject);
        }
    }

    public static class MapExtensions
    {
        public static IQueryable<TDestination> Map<TSource, TDestination>(this IQueryable<TSource> aSource, Expression<Func<TSource, TDestination>> aMapper)
        {
            return aSource.Select(aMapper);
        }
        public static IEnumerable<TDestination> Map<TSource, TDestination>(this IEnumerable<TSource> aSource, Func<TSource, TDestination> aMapper)
        {
            return aSource.Select(aMapper);
        }
    }
}