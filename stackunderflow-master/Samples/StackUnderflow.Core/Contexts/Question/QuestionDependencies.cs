using System;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionDependencies
    {
        public Func<SendConfirmationLetter, TryAsync<SendConfirmationAck>> QuestionEmail { get; set; }
    }
}