/* 1. Create Connection - 
        This will establish/create a new SignalR connection with SignalR Hub at the specified URL [/Hubs/userCount] 
*       This is a connection string
*/
var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/Hubs/userCount").build();

/* 2. Connect To Methods That Hub invokes AKA Receive Notification From Hub 
*       This will execute when the Hub will send the notification to all the clients
*       On method (First parameter) - Copy the name from the method SendAsync in Hub and paste it here
*       GetElementById parameter (ID Name) - Copy the ID name from Index.cshtml and paste it here
*/
connectionUserCount.on("UpdateTotalViews", function (viewCount) {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = viewCount.toString();
});

connectionUserCount.on("UpdateTotalUsers", function (userCount) {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = userCount.toString();
});

/* 3. Invoke Hub Method AKA Send Notification To Hub 
*       This will execute when Hub will send the notification to all the clients
*/
async function NewWindowLoadedOnClientAsync() {
    await connectionUserCount.send("NewWindowLoadedAsync");
}

function fulfilled() {
    console.log("Connected to SignalR Hub");
    NewWindowLoadedOnClientAsync();
}

function rejected() {
   
}

/* 4. Start Connection */
connectionUserCount.start().then(fulfilled, rejected);