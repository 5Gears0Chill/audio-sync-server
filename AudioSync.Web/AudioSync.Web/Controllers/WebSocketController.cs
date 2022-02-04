using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AudioSync.Web.Controllers
{
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        static List<Data> dataList = new List<Data>(); 

        [HttpGet("/ws")]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                                   HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(HttpContext, webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                var arraySegment = new ArraySegment<byte>(buffer);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var data = GetDataFromString(arraySegment);
                    if (data != null)
                    {
                        if (!dataList.Any(x => x.DeviceId == data.DeviceId))
                        {
                            dataList.Add(data);
                            var bytes = Encoding.Default.GetBytes($"Added {dataList.Count}");
                            var arraySegmentResponse = new ArraySegment<byte>(bytes);
                            await webSocket.SendAsync(arraySegmentResponse, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else
                        {
                            var bytes = Encoding.Default.GetBytes($"Device Id Already Exists In Data Set");
                            var arraySegmentResponse = new ArraySegment<byte>(bytes);
                            await webSocket.SendAsync(arraySegmentResponse, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        
                    }
                }
            }

            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }


        private Data GetDataFromString(ArraySegment<byte> arraySegment)
        {
            var message = Encoding.Default.GetString(arraySegment).TrimEnd('\0');
            if (!string.IsNullOrWhiteSpace(message))
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Data>(message);
                    return data;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private async Task SendMessageToSockets(string message)
        {

        }
    }

    public class Data
    {
        public string DeviceId { get; set; }
        public long Time { get; set; }
    }

}


