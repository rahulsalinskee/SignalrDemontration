using Microsoft.AspNetCore.SignalR;

namespace SignalDemontration.Hubs
{
    public class UserHub : Hub
    {
        /* This is to track the total number of views website gets */
        public static int TotalView { get; set; } = 0;

        /// <summary>
        /// This will be called every time when page loads or refreshed
        /// </summary>
        public async Task NewWindowLoadedAsync()
        {
            TotalView++;

            /* This will send the total number of views to all connected clients 
            * Method: "UpdateTotalViews" is a custom method in JavaScript client side
            */
            await Clients.All.SendAsync(method: "UpdateTotalViews", TotalView);
        }
    }
}
