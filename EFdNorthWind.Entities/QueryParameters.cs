namespace EFdNorthWind.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;

    public class QueryParameters<T>
    {
        public QueryParameters()
        {
            Includes = null;
            Where = null;
            OrderBy = null;
            OrderByDescending = null;
        }

        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }
        public Func<T, object> OrderBy { get; set; }
        public Func<T, object> OrderByDescending { get; set; }

    }
}
