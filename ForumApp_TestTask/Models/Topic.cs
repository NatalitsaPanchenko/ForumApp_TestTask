using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumApp_TestTask.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}