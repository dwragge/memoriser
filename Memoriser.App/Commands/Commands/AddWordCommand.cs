namespace Memoriser.App.Commands.Commands
{
    public class AddWordCommand : ICommand
    {
        public string Word { get; set; }
        public string[] AcceptedAnswers { get; set; }

        public AddWordCommand(string word, string[] acceptedAnswers)
        {
            Word = word;
            AcceptedAnswers = acceptedAnswers;
        }
    }
}
