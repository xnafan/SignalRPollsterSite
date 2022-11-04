using Microsoft.AspNetCore.SignalR;

namespace SignalRPollsterSite.Hubs
{
    public class PollHub : Hub
    {

        public async Task Vote(string pollId, int vote)
        {
            await Clients.All.SendAsync("VoteUpdated",  );
        }

    }
}
