using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner
{
    class ConfirmationAck
    {
        public class SendConfirmationAck
        {
            public string Confirmation { get; private set; }

            public SendConfirmationAck(string confirmation)
            {
                this.Confirmation = confirmation;
            }
        }
    }
}