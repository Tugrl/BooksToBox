﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace BooksToBoxDemo.Models.ViewModels
{
    public class BookAddRequests
    {
        public string? BookName { get; set; }
        public string? Author { get; set; }
        public string? BookImage { get; set;}
        public IEnumerable<SelectListItem>? Categories { get; set;}
        public string[] SelectedCategories { get; set; }=Array.Empty<string>();



    }
}
