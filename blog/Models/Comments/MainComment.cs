﻿using System.Collections.Generic;

namespace blog.Models.Comments
{
    public class MainComment: Comment
    {
        public List<SubComment> SubComments { get; set; }
    }
}
