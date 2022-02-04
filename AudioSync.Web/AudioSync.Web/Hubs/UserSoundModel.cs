using System;
namespace AudioSync.Web.Hubs
{
    public class UserSoundModel
    {
        public DateTime TimeOfReceiving { get; set; }
        public string UserIdentifier { get; set; }
    }
}

