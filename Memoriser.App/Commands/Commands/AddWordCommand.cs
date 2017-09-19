using System;
using System.Linq;

namespace Memoriser.App.Commands.Commands
{
    public class AddWordCommand : ICommand
    {
        public string Word { get; set; }
        public string[] AcceptedAnswers { get; set; }

        public AddWordCommand(string word, string[] acceptedAnswers)
        {
            Word = word.ReduceWhitespace().ToLowerInvariant();
            AcceptedAnswers = acceptedAnswers.Select(x => x.ReduceWhitespace().ToLowerInvariant()).ToArray();
        }
    }
}
