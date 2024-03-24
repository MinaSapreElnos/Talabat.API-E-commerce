using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Spacifications;

namespace Talabat.Repository
{
    internal class SpacificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //ده كلاس فيه الفانكشن الي بتبني الكويري //
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery , ISpacification<TEntity> Spac)
        {
            var Query = InputQuery;

            if (Spac.Criteria is not null)
                Query= Query.Where(Spac.Criteria);

            if (Spac.OrderBy is not null)
                Query = Query.OrderBy(Spac.OrderBy);

            else if (Spac.OrderByDesc is not null)
                Query = Query.OrderByDescending(Spac.OrderByDesc);

            if (Spac.IsPagianationEnable)
            {
                Query=Query.Skip(Spac.Skip).Take(Spac.Take);
            }

            Query=Spac.Includes.Aggregate(Query, (CarrentQuery , includeExpression) => CarrentQuery.Include(includeExpression));

            return Query;


        }

    }
}
