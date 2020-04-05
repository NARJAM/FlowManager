using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace AccountManager.Hubs
{ 
    [HubName("flow")]
    public class FlowHub : Hub
    {
        public override Task OnConnected()
        {
            string gameAuth = Context.QueryString["gameAuth"];

            if (gameAuth == "Server")
            {
                Groups.Add(Context.ConnectionId, "servers");
            }
            else
            {
                Groups.Add(Context.ConnectionId, "clients");
            }

            Clients.Caller.OnSelfJoined(Context.ConnectionId, gameAuth);

            Clients.Others.OnSessionJoined(Context.ConnectionId, gameAuth);
            
            return base.OnConnected();
        }
        
        public override Task OnDisconnected(bool stopCalled)
        {
                Groups.Remove(Context.ConnectionId, "servers");
                Groups.Remove(Context.ConnectionId, "clients");
                Clients.All.OnSessionLeft(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public void SendToServer(string eventName, string eventData)
        {
            Clients.Group("servers").ReceiveFromClient(eventName, Context.ConnectionId, eventData);
        }

        public void SendToClients(string eventName, string eventData)
        { 
            Clients.Group("clients").ReceiveFromServer(eventName, Context.ConnectionId, eventData);
        }
        
        public void ErrorLog(string message)
        {
            Clients.All.ErrorReceived(message);
        }
    }
}