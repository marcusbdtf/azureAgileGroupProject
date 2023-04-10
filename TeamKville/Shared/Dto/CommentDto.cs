using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Dto
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public DateTime Date { get; set; }
    }
}
