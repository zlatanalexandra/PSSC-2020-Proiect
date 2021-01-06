using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CheckLanguage
{
    public class CheckLanguageCmd
    {
        public string Text { get; }
        public CheckLanguageCmd(string text)
        {
            Text = text;
        }
    }
}
