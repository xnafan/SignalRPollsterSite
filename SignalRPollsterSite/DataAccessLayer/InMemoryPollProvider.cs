using System.Linq;
namespace SignalRPollsterSite.DataAccessLayer;

public class InMemoryPollProvider : IPollProvider
{
    private Dictionary<string, Poll> _polls = new()
    {
        { "1234", new Poll(id : "1234",
            description : "What's your favorite fruit?",
            title : "Fruit poll",
            choices : new List<string> { "Apples", "Bananas", "Cherries","Water melons"})}
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