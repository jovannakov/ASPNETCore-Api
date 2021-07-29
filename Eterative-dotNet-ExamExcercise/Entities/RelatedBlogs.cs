using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Entities
{
    public class RelatedBlogs
    {
        public int BaseBlog { get; set; }
        public int RelatedTo { get; set; }
        public Blog BaseBlogObj { get; set; }
        public Blog RelatedToObj { get; set; }
    }
}
