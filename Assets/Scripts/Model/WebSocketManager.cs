using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


public class WebSocketManager : MonoBehaviour
{
    private WebSocket ws;
    private string url = "ws://10.6.5.93:9001/ws?idpersonne=1";
    private Player player;

    public static WebSocketManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeWebSocket();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void InitializeWebSocket()
    {
        ws = new WebSocket(url);
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection opened.");
        };
        ws.OnMessage += (sender, e) =>
        {
           Debug.Log("Message : " + e.Data);
           try {
            MainThreadDispatcher.Instance().Enqueue(() =>{

                Message receivedMessage = JsonConvert.DeserializeObject<Message>(e.Data);
                Player.Instance.AddMessage(receivedMessage);

            });
               
            }
            catch (JsonException ex)
            {
                Debug.LogError("JSON deserialization error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Unexpected error: " + ex.Message);
            }
        };
        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket error: " + e.Message);
        };
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket connection closed.");
        };
        ws.Connect();
    }

    void OnApplicationQuit()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }

    public void SendMessage(Message message)
    {
        if (ws != null && ws.IsAlive)
        {
            string jsonMessage = JsonConvert.SerializeObject(message);
            ws.Send(jsonMessage);
        }
        else
        {
            Debug.LogError("WebSocket is not connected.");
        }
    }

    void Update()
    {
        // Vous pouvez ajouter des mises à jour ici si nécessaire
    }
}
