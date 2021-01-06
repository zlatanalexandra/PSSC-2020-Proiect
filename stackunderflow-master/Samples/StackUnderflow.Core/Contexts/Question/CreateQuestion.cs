using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public class CreateQuestion
    {
        public string Question { get; set; }
        public string Body { get; set; }
        public string Tags { get; set; }

        public CreateQuestion() { }
        public CreateQuestion(string question, string body, string tags)
        {
            this.Question = Question;
            this.Body = body;
            this.Tags = tags;
        }
    }
}
