﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class UpdateCommentRequest
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
