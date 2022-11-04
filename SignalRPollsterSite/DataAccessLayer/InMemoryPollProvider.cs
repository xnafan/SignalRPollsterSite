using SignalRPollsterSite.Model;

namespace SignalRPollsterSite.DataAccessLayer
{
    public class InMemoryPollProvider : IPollProvider
    {
        private Dictionary<string, Poll> _polls = new();
        public Poll? GetPollInfo(string pollId)
        {
            if(_polls.ContainsKey(pollId)) { return _polls[pollId]; }
            return null;
        }

        public PollResult? Vote(string pollId, int choiceNumber)
        {
            Poll? poll = GetPollInfo(pollId);
            if(poll != null) {
                
                return poll.Result;
            }
        }
    }
}
