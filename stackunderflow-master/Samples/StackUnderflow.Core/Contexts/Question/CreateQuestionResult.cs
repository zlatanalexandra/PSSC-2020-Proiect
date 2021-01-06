using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string Question { get; private set; }
            public string Body { get; private set; }
            public string Tags { get; private set; }

            public QuestionCreated(Guid questionId, string title, string description, string tags)
            {
                this.QuestionId = questionId;
                this.Question = title;
                this.Body = description;
                this.Tags = tags;
            }
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Message { get; }
            public QuestionNotCreated(string message)
            {
                Message = message;
            }
        }
    }
}
