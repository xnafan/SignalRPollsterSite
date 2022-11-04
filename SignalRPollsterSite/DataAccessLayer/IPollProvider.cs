using SignalRPollsterSite.Model;

namespace SignalRPollsterSite.DataAccessLayer
{
    public interface IPollProvider
    {
        Poll? GetPollInfo(string pollId);
        PollResult? Vote(string pollId, int vote);
    }
}
