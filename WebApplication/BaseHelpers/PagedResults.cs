﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication
{
    public class PagedResults<T>
    {
        /// <summary>
        /// The page number this page represents. 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary> 
        /// The size of this page. 
        /// </summary> 
        public int PageSize { get; set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        public int TotalNumberOfRecords { get; set; }

        /// <summary> 
        /// The URL to the next page - if null, there are no more pages. 
        /// </summary> 
        public string NextPageUrl { get; set; }

        /// <summary> 
        /// The records this page represents. 
        /// </summary> 
        public IEnumerable<T> Results { get; set; }

        /// <summary> 
        /// The count of results. 
        /// </summary> 
        public int ResultCount { get;  set; }
    }
}
