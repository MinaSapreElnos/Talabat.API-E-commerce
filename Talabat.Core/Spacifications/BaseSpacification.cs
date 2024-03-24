using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Talabat.Core.Models;

namespace Talabat.Core.Spacifications
{
    public class BaseSpacification<T> : ISpacification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; set; }

        public Expression<Func<T, object>> OrderByDesc { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }

        public bool IsPagianationEnable { get; set; }

        public BaseSpacification()
        {
            
        }

        public BaseSpacification(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }


        public void AddOrderBy(Expression<Func<T, object>>  orderByexpression)
        {
            OrderBy = orderByexpression;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderByDescexpression)
        {
            OrderByDesc = orderByDescexpression;
        }

        public void ApplyPagination( int _Skip, int _Take)
        {
            IsPagianationEnable = true;

            Take = _Take;

            Skip = _Skip;
        }


    }
}

