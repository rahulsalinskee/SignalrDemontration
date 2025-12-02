using Microsoft.AspNetCore.SignalR;

namespace SignalDemontration.Hubs
{
    public class UserHub : Hub
    {
        /* This is to track the total number of views website gets */
        public static int TotalView { get; set; } = 0;

        /* This is to track the total number of users connected */
        public static int TotalUsers { get; set; } = 0;

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

        /// <summary>
        /// This will be called every time when the user count is changed
        /// </summary>
        /// <returns></returns>
        private async Task UpdateTotalUsersAsync()
        {
            /* This will send the total number of Users to all the connected clients 
            * Method: "UpdateTotalUsers" is a custom method in JavaScript client side
            */
            await Clients.All.SendAsync(method: "UpdateTotalUsers", TotalUsers);
        }

        /// <summary>
        /// This will be called every time when a new user is connected
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            UpdateTotalUsersAsync().GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// This will be called every time when a user is disconnected
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            UpdateTotalUsersAsync().GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
