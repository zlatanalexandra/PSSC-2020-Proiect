using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendAckToOwner
{
    class SendConfirmationNotification
    {
        public class SendConfirmationLetter
        {
            public string Email { get; set; }
            public string ConfirmationMsg { get; set; }

            public SendConfirmationLetter(string email, string confirmationMeg)
            {
                this.Email = email;
                this.ConfirmationMsg = confirmationMeg;
            }

        }
    }
}