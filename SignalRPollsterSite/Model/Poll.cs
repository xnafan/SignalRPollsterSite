namespace SignalRPollsterSite.Model
{
    public class Poll
    {
        private string? id;

        public string? Id { get => id; 
            set { 
                id = value;
                Result.PollId = Id;
            } }
        public string Title { get; set; } = "no title";
        public string Description { get; set; } = "no description";
        public List<string> Choices { get; set; } = new List<string>();
        public PollResult Result { get; set; }

        public Poll(string? id, string title, string description, List<string> choices)
        {
            Id = id;
            Title = title;
            Description = description;
            Choices = choices;
            Result = new PollResult(Id, new List<int>(choices.Count));
        }

        public int Vote(int choiceNumber)
        {
            if (choiceNumber >= Choices.Count) { throw new ArgumentOutOfRangeException($"No choice number {choiceNumber}! Only {Choices.Count} choices in Poll."); }
            Result.Votes[choiceNumber]++;
            return Result.Votes[choiceNumber];
        }
    }
}