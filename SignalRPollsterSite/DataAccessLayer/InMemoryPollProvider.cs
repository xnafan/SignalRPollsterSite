using System.Linq;
namespace SignalRPollsterSite.DataAccessLayer;

public class InMemoryPollProvider : IPollProvider
{
    private Dictionary<string, Poll> _polls = new()
    {
        { "4ER-8IK", new Poll(id : "4ER-8IK",
            description : "Nice poll!",
            title : "Best poll ever",
            choices : new List<string> { "Choice 1", "Choice 2", "Choice 3", "Choice 4" })}
    };

    public string CreatePoll(Poll poll)
    {
        string pollId = ShortUIDTool.CreateShortId();
        _polls[pollId] = poll;
        return pollId;
    }

    public Poll GetPollInfo(string pollId) => _polls[pollId];
    public IEnumerable<int> Vote(string pollId, int choiceNumber) => GetPollInfo(pollId).Results;

    int[] IPollProvider.Vote(string pollId, int vote)
    {
        _polls[pollId].Vote(vote);
        return _polls[pollId].Results.ToArray();
    }
}