using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JEngine.Realtime
{
    [Serializable]
    public class Client
    {
        /// <summary>
        /// A unique ID for Client
        /// </summary>
        private string ID;

        /// <summary>
        /// ID Setter
        /// </summary>
        /// <param name="value"></param>
        public void SetID(string value)
        {
            ID = value;
            LoggedIn = false;
        }

        /// <summary>
        /// Whether client has logged in or not
        /// </summary>
        private bool LoggedIn = false;


        /// <summary>
        /// Login to the server
        /// </summary>
        public void LogIn()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["id"] = ID
            };
            Chatroom.Socket.Emit("login", new JSONObject(data), (res) =>
            {
                if (res.list[0].str != ID)
                {
                    Chatroom.LogError("Your ID had been used, new ID: "+ res.list[0].str);
                }
                SetID(res.list[0].str);
                LoggedIn = true;
            });
        }



        /// <summary>
        /// Send Message
        /// </summary>
        /// <param name="msg">Text message</param>
        public async void SendMessage(string msg)
        {
            //If hasnt logged in yet, try to log in
            if (!LoggedIn)
            {
                await Task.Run(LogIn);
            }
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                ["id"] = ID,
                ["text"] = msg
            };
            Chatroom.Socket.Emit("sendMsg", new JSONObject(data), (res) =>
              {
                  if (res.list.Count > 0)
                  {
                      Chatroom.LogError(res.list[0].str);
                  }
              });
        }
    }
}