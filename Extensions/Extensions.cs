using System.Linq.Expressions;
using System.Reflection;

namespace AveryBusiness;

public static class Extensions
{
    public static IQueryable<TSource> Search<TSource>(this IQueryable<TSource> source, string search, string[] parameters)
    {
        return source.Where(SearchPredicate<TSource>(search, parameters));
    }

    private static Expression<Func<T, bool>> SearchPredicate<T>(string search, string[] parameters)
    {
        var props = typeof(T).GetProperties();
        var nonIntegerProps = typeof(T).GetProperties().Where(pi => pi.PropertyType != typeof(int));

        Expression<Func<T, bool>> expression = (item => false);
        expression = expression.Or(PredicateFromProperties<T>(props, parameters, search));
        return expression;
    }
    private static Expression<Func<T, bool>> PredicateFromProperties<T>(IEnumerable<PropertyInfo> properties, string[] parameters, string search)
    {
        Expression<Func<T, bool>> expression = (item => false);
        foreach (var p in parameters)
        {
            var i = p.Split('.');
            if (i.Length == 2)
            {
                var t = properties.FirstOrDefault(pi => pi.Name == i[0]);
                PropertyInfo[] ps = t.PropertyType.GetProperties();
                PropertyInfo propertyInfo = ps.FirstOrDefault(pi => pi.Name == i[1]);
                if (propertyInfo != null)
                {
                    Expression<Func<T, bool>> exp = item => (propertyInfo.GetValue(t.GetValue(item)).ToString() ?? "").Contains(search, StringComparison.InvariantCultureIgnoreCase);
                    expression = expression.Or(exp);
                }
            }
            else
            {
                PropertyInfo propertyInfo = properties.FirstOrDefault(pi => pi.Name == p);
                if (propertyInfo != null)
                {
                    Expression<Func<T, bool>> exp = item => (propertyInfo.GetValue(item).ToString() ?? "").Contains(search, StringComparison.CurrentCultureIgnoreCase);
                    expression = expression.Or(exp);
                }
            }
        }
        return expression;
    }
}
