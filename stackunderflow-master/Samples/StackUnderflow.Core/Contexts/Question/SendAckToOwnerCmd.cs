using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner
{
    public class SendAckToOwnerCmd
    {
        public int QuestionId { get; }
        public int QuestionOwnerId { get; }
        public SendAckToOwnerCmd(int questionId, int questionOwnerId)
        {
            QuestionId = questionId;
            QuestionOwnerId = questionOwnerId;
        }
    }
}
