using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public Expression<Func<T, object>> OrderBy { get;  set; }
        public Expression<Func<T, object>> OrderByDescending { get;  set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public int SkipSize { get; set; }
        public int TakeSize { get; set; }
        public bool IsPaginationEnables { get; set; }

        public BaseSpecification()
        {
                
        }
        public BaseSpecification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }


        public void ApplyPagination(int skip, int take)
        {
            IsPaginationEnables = true;
            TakeSize = take;
            SkipSize = skip;
        }
    }
}
