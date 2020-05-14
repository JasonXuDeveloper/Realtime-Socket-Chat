using UnityEngine;
using UnityEngine.UI;
using SocketIO;

namespace JEngine.Realtime
{
    public class Chatroom : MonoBehaviour
    {
        /// <summary>
        /// Socket
        /// </summary>
        public static SocketIOComponent Socket;


        /// <summary>
        /// Boolean of has socket been connected
        /// </summary>
        public bool Connected;

        /// <summary>
        /// Is Debug or not
        /// </summary>
        public bool IsDebug;


        /// <summary>
        /// Local Client
        /// </summary>
        public Client Client;


        /// <summary>
        /// Chatroom UI
        /// </summary>
        public GameObject ChatroomUI;

        /// <summary>
        /// Start UI
        /// </summary>
        public GameObject StartUI;

        /// <summary>
        /// UI Component which inputs name
        /// </summary>
        public InputField NameField;

        /// <summary>
        /// UI Button which sends login request
        /// </summary>
        public Button LoginButton;

        /// <summary>
        /// UI which shows the messages
        /// </summary>
        public GameObject UIContentArea;


        /// <summary>
        /// Prefab of the message
        /// Direction: Prefab/Chat Message Prefab
        /// Double click on prefab in unity to make changes
        /// </summary>
        public GameObject MessagePrefab;

        /// <summary>
        /// UI Component which inputs text
        /// </summary>
        public InputField TextField;

        /// <summary>
        /// UI Button which sends message
        /// </summary>
        public Button SendButton;

        /// <summary>
        /// Initialize socket instance and the client setup
        /// </summary>
        private void Awake()
        {
            Socket = FindObjectOfType<SocketIOComponent>();
            Connected = false;
            Client = new Client();
        }

        /// <summary>
        /// Setup listeners and start chat rrom
        /// </summary>
        void Start()
        {
            //On Soccket connected
            Socket.On("open",(e)=>
            {
                //Because this callback will be called twice as one for connecting and one for connected,
                //we need to make sure our code runs when its has been conencted, not in connecting
                if (!Connected)
                {
                    Connected = true;
                    return;
                }
                if (IsDebug)
                {
                    Debug.Log("[Socket Connection] Connected, congrats!");
                }
            });

            //On Soccket error
            Socket.On("error", (e) =>
            {
                if (IsDebug)
                {
                    Debug.LogError("[Socket Connection] Something went wrong...");
                }
            });

            //On Soccket closed
            Socket.On("close", (e) =>
            {
                if (IsDebug)
                {
                    Debug.Log("[Socket Connection] Socket has been closed successfully");
                }
            });

            //On recieved messages from server
            Socket.On("recieveMsg", (e) =>
            {
                //Debug.Log(e.data);

                /*
                 * Make message show on UI
                 */
                var id = e.data.list[0].str;
                var text = e.data.list[1].str;

                //show message on UI
                var go = Instantiate(MessagePrefab, parent: UIContentArea.transform);

                //if is message from system, make message red
                if(id == "System")
                {
                    go.GetComponent<Text>().color = new Color(220,0,0);
                }
                go.GetComponent<Text>().text = "[" + id + "] " + text;
            });

            //Button click listener
            SendButton.onClick.RemoveAllListeners();
            SendButton.onClick.AddListener(() =>
            {
                SendMessage();
            });

            LoginButton.onClick.RemoveAllListeners();
            LoginButton.onClick.AddListener(() =>
            {
                //Set name and login
                StartUI.SetActive(false);
                Client.SetID(NameField.text);
                Client.LogIn();
                //Start Chatroom
                ChatroomUI.SetActive(true);
            });
        }

        /// <summary>
        /// A method which reads input text message and send it
        /// </summary>
        private void SendMessage()
        {
            if (TextField.text != "")
            {
                Client.SendMessage(TextField.text);
                TextField.text = "";
            }
        }

        /// <summary>
        /// When client press return, send message automatically
        /// </summary>
        private void Update()
        {
            if (!Connected) return;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage();
            }
        }


        /// <summary>
        /// Static method which logs error onto unity's console
        /// </summary>
        /// <param name="err"></param>
        public static void LogError(string err)
        {
            Debug.LogError("[Chatroom ERROR] " + err);
        }
    }
}