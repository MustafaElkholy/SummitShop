using Microsoft.EntityFrameworkCore;
using SummitShop.Core.Entities;
using SummitShop.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Repository
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery /* DbSet*/, ISpecification<TEntity> spec)
        {
            var query = inputQuery; // dbcontext.Products
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria); // dbcontext.Products.where(p=>p.Id == id)
            }



            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPaginationEnables)
            {
                query = query.Skip(spec.SkipSize).Take(spec.TakeSize);
            }

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
           

            return query;

        }

    }
}
