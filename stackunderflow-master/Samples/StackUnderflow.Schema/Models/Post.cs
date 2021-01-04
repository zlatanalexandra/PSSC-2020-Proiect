using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackUnderflow.EF.Models
{
    public partial class Post
    {
        public Post()
        {
            InversePostNavigation = new HashSet<Post>();
            PostTag = new HashSet<PostTag>();
            PostView = new HashSet<PostView>();
            Vote = new HashSet<Vote>();
        }

        public int TenantId { get; set; } // reply and question context
        public int PostId { get; set; } // reply and question context
        public byte PostTypeId { get; set; }
        public int? ParentPostId { get; set; }
        public string Title { get; set; } // question title
        public string PostText { get; set; } // reply and question context (question body - description)
        public Guid PostedBy { get; set; } // reply context
        public bool AcceptedAnswer { get; set; } // reply context
        public DateTime DateCreated { get; set; } // create question context - cand a fost create intrebarea
        public bool Closed { get; set; } // reply context
        public Guid? ClosedBy { get; set; }
        public DateTime? ClosedDate { get; set; } // reply context
        public Guid? LastUpdatedBy { get; set; } // reply context
        public Guid RowGuid { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime SysStartTime { get; set; }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
        public DateTime SysEndTime { get; set; }

        public virtual Post PostNavigation { get; set; }
        public virtual PostType PostType { get; set; }
        public virtual TenantUser TenantUser { get; set; } // reply context - username/email cine posteazza raspunsul
        public virtual TenantUser TenantUser1 { get; set; }
        public virtual TenantUser TenantUserNavigation { get; set; }
        public virtual ICollection<Post> InversePostNavigation { get; set; }
        public virtual ICollection<PostTag> PostTag { get; set; } // tagurile intrebarilor
        public virtual ICollection<PostView> PostView { get; set; }
        public virtual ICollection<Vote> Vote { get; set; } // reply context

        //public List<Post> ChildPosts { get; set; }
    }
}
