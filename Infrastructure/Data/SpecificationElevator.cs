﻿using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    internal class SpecificationElevator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputquery,ISpecification<TEntity> spec)

        {
            var query = inputquery;
            if (spec.Criteria !=null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query,(current,include)=> current.Include(include));

            return query;
        }


    }
}
