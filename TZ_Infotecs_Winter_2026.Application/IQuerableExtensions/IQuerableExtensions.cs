using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TZ_Infotecs_Winter_2026.Application.IQuerableExtensions
{
    public static class IQuerableExtensions
    {
        public static IQueryable<T> ApplyEqualityFilter<T, TValue>(
            this IQueryable<T> query,
            Expression<Func<T, TValue>> selector,
            TValue? value)
            where TValue : class?
        {
            if (value == null)
                return query;

            var param = Expression.Parameter(typeof(T), "r");
            var property = Expression.Invoke(selector, param);
            var constant = Expression.Constant(value, typeof(TValue));
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equals, param);

            return query.Where(lambda);
        }
        //Для типов значений. Для текущих фильтров не требуется данный метод расширения
        public static IQueryable<T> ApplyValueEqualityFilter<T, TValue>(
            this IQueryable<T> query,
            Expression<Func<T, TValue>> selector,
            TValue? value)
            where TValue : struct
        {
            if (!value.HasValue)
                return query;

            var param = Expression.Parameter(typeof(T), "r");
            var property = Expression.Invoke(selector, param);
            var constant = Expression.Constant(value.Value, typeof(TValue));
            var equals = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<T, bool>>(equals, param);

            return query.Where(lambda);
        }
        public static IQueryable<T> ApplyRangeFilter<T, TValue>(
            this IQueryable<T> query,
            Expression<Func<T, TValue>> selector,
            TValue? min,
            TValue? max)
            where TValue : struct, IComparable<TValue>
        {
            if (!min.HasValue && !max.HasValue) return query;

            var param = selector.Parameters[0];
            var property = selector.Body;

            Expression? body = null;

            if (min.HasValue)
                body = Expression.GreaterThanOrEqual(property, Expression.Constant(min.Value));

            if (max.HasValue)
            {
                var less = Expression.LessThanOrEqual(property, Expression.Constant(max.Value));
                body = body == null ? less : Expression.AndAlso(body, less);

            }

            if (body == null) return query;

            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return query.Where(lambda);
        }
    }
}
