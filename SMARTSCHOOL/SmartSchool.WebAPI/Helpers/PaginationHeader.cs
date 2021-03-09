using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Helpers
{
    public class PaginationHeader
    {
        public int ItemsPerPage { get; set; }
        public int TotalIems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }        

        public PaginationHeader(int currentPage, int itemsPerPage, int totalIems, int totalPages) 
        {
            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
            this.TotalIems = totalIems;
            this.TotalPages = totalPages;               
        }
    }
    
}
