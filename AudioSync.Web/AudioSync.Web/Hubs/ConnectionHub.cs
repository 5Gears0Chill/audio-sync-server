using AudioSync.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;

namespace AudioSync.Web.Hubs
{
    public class ConnectionHub : Hub
    {
        //#region Data Members

        //static List<DeviceDetail> ConnectedDevices = new List<DeviceDetail>();
        //static List<MessageDetail> CurrentMessage = new List<MessageDetail>();

        //#endregion
        //#region Methods
        //public void Connect(string deviceId)
        //{
        //    var id = Context.ConnectionId;


        //    if (ConnectedDevices.Count(x => x.ConnectionId == id) == 0)
        //    {
        //        ConnectedDevices.Add(new DeviceDetail { ConnectionId = id, DeviceId = deviceId });

        //        // send to caller
        //        Clients.Caller.OnConnected(id, userName, ConnectedDevices, CurrentMessage);

        //        // send to all except caller client
        //        Clients.AllExcept(id).onNewUserConnected(id, userName);

        //    }

        //}

        //#endregion
    }
}
