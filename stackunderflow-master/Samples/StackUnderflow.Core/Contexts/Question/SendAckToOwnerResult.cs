using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner
{
    [AsChoice]
    public static partial class SendAckToOwnerResult
    {
        public interface ISendAckToOwnerResult { }

        public class AcknowledgementSent : ISendAckToOwnerResult
        {
            public int QuestionId { get; }
            public int QuestionOwnerId { get; }
            public AcknowledgementSent(int questionId, int questionOwnerId)
            {
                QuestionId = questionId;
                QuestionOwnerId = questionOwnerId;
            }
        }

        public class AcknowledgmentNotSent : ISendAckToOwnerResult
        {
            public string Message { get; }
            public AcknowledgmentNotSent(string message)
            {
                Message = message;
            }
        }

    }
}
