//using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<Post> Questions { get; }
        public QuestionWriteContext(ICollection<Post> questions)
        {
            Questions = questions ?? new List<Post>();
        }
    }
}