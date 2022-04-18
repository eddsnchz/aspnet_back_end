﻿using System;
using System.Linq;
using BACKEND.DTOs;

namespace BACKEND.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable.Skip((paginationDTO.Page - 1) * paginationDTO.RecordsByPage)
                .Take(paginationDTO.RecordsByPage);
        }
    }
}
