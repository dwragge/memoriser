using System;
using System.Collections.Generic;
using System.Linq;

namespace Memoriser.ApplicationCore.Models
{
    public class LearningItem
    {
        private string _acceptedAnswers;

        public Guid Id { get; set; }
        public string ToBeGuessed { get; private set; }

        public IList<string> AcceptedAnswers
        {
            get => _acceptedAnswers?.Split(',').Select(x => x.Trim()).ToList().AsReadOnly();
            set => _acceptedAnswers = string.Join(',', value);
        }

        public RepetitionInterval Interval { get; set; }

        public LearningItem(string word, string[] acceptedAnswers)
        {
            ToBeGuessed = word;
            _acceptedAnswers = string.Join(',', acceptedAnswers);
            Interval = RepetitionInterval.NewDefault();
            Id = Guid.NewGuid();
        }

        public LearningItem(string word, string answer)
            : this(word, new[] { answer })
        {
        }

        private LearningItem()
        {
        }
    }
}
