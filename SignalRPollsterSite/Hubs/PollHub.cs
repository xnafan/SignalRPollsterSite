using Microsoft.AspNetCore.SignalR;
namespace SignalRPollsterSite.Hubs;
public class PollHub : Hub
{
    private IPollProvider _pollProvider;
    public PollHub(IPollProvider pollProvider) => _pollProvider = pollProvider;
    public async Task GetPollInfo(string pollId) => await Clients.Caller.SendAsync("ReceivePollInfo", _pollProvider.GetPollInfo(pollId));
    public async Task CreatePoll(Poll poll) => await Clients.Caller.SendAsync("PollCreated", _pollProvider.CreatePoll(poll));
    public async Task Vote(string pollId, int vote) => await Clients.All.SendAsync("VoteUpdated", _pollProvider.Vote(pollId, vote));
}