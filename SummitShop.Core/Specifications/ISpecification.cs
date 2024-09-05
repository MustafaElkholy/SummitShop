using SummitShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SummitShop.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T,bool>> Criteria { get; set; }  // How to filter (where condition) (p=>p.Id==id)
        public List<Expression<Func<T,object>>> Includes { get; set; } // how to include 
        public Expression<Func<T, object>> OrderBy { get; set; } 
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        public int SkipSize { get; set; }
        public int TakeSize { get; set; }
        public bool IsPaginationEnables { get; set; }

    }
}
