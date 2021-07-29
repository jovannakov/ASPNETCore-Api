using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Entities
{
    public class Blog
    {
        public Blog()
        {
            this.RelatedToBlogs = new List<RelatedBlogs>();
            this.RelatedBlogs = new List<RelatedBlogs>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public virtual ICollection<RelatedBlogs> RelatedToBlogs { get; set; }
        public virtual ICollection<RelatedBlogs> RelatedBlogs { get; set; }
    }
}
