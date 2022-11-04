namespace SignalRPollsterSite.Model
{
    public class PollResult
    {
        public string PollId { get; set; }
        public List<int> Votes { get; set; } = new List<int>();
public PollResult(string pollId, List<int> votes)
        {
            PollId = pollId;
            Votes = votes;
        }
    }
}