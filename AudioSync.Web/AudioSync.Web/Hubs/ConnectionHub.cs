using AudioSync.Core.Models;
using AudioSync.Core.Utils;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AudioSync.Web.Hubs
{
    public class ConnectionHub : Hub
    {
        #region Data Members
        static List<string> AvailableGroups = new List<string>();
        static string Phrase = string.Empty;
        static DateTime timeOfSoundPlayed = DateTime.MinValue;
        static List<UserSoundModel> userSoundModels = new List<UserSoundModel>();
        #endregion

        #region Methods

        /// <summary>
        /// OnConnect add connection to user list
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            UserSoundModel user = new UserSoundModel()
            {
                UserIdentifier = connectionId,
                TimeOfReceiving = DateTime.Now
            };
            userSoundModels.Add(user);            
            await Clients.All.SendAsync("ReceiveMessage", string.Format("User {0} joined at {1}", connectionId, DateTime.Now));
            //await Clients.All.SendAsync("ReceiveObject", userSoundModels);            
        }

        /// <summary>
        /// OnDisconnect remove connection from user list
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            string connectionId = Context.ConnectionId;
            var userToRemove = userSoundModels.SingleOrDefault(u => u.UserIdentifier.Equals(connectionId));
            if(userToRemove != null)
            {
                userSoundModels.Remove(userToRemove);
                await Clients.All.SendAsync("ReceiveObject", userSoundModels);
            }
        }

        public async Task<bool> JoinGroup(string groupName)
        {
            if (AvailableGroups.Contains(groupName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName)
                    .SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// creates and returns group. adds user to group
        /// </summary>
        /// <param name="phrase">the message you want to send to the group</param>
        /// <returns>group name</returns>
        public async Task<string> CreateGroup(string phrase)
        {
            var groupName = StringUtils.RandomString(6);
            await Groups.AddToGroupAsync(Context.ConnectionId,groupName);
            AvailableGroups.Add(groupName);
            Phrase = Regex.Replace(phrase.Trim(), @"[^0-9a-zA-Z]+", "");
            await Clients.Client(Context.ConnectionId).SendAsync(groupName);
            return groupName;
        }

        public void SendPhraseToUserInWordFormat()
        {
            string[] words = Phrase.Split(" ");
            
            foreach(var u in userSoundModels)
            {
                var latency = u.TimeOfReceiving - timeOfSoundPlayed;
                var startingOffset = 100;
                foreach (string word in words)
                {
                    startingOffset += 100;
                    BackgroundJob.Schedule(() =>
                    SendPhraseToUsers(word, u.UserIdentifier),
                    TimeSpan.FromMilliseconds(latency.TotalMilliseconds + startingOffset));
                }
            }
        }

        public void SendPhraseToUsers(string word,string userIdentifier)
        {
            Clients.User(userIdentifier).SendAsync("ReceivePhraseWord", word);
                  
        }

        public async Task PlaySound()
        {
            timeOfSoundPlayed = DateTime.Now;
            await Clients.All.SendAsync("SoundPlayed", string.Format("Sound played at {0}", timeOfSoundPlayed));
        }

        public void ReportSoundPlayed(string userIdentifier)
        {
            userSoundModels.Add(new UserSoundModel
            {
                UserIdentifier = userIdentifier,
                TimeOfReceiving = DateTime.Now,
            });
        }
        #endregion
    }
}