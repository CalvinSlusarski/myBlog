using Microsoft.EntityFrameworkCore;
using Code.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code.Models
{
    public class Post
    {
        public Post() { }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PostDate { get; set; }
        public string Signature { get; set; }
    }
}
