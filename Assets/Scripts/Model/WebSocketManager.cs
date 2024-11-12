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
    private string url = "wss://lamb-master-vulture.ngrok-free.app/ws?idpersonne=1";
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

        if (player == null)
        {
            Debug.LogError("Player instance not found in the scene.");
        }
    }

    void InitializeWebSocket()
    {
        ws = new WebSocket(url);
        ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket connection opened.");
        };
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message : " + e.Data);
            
                MainThreadDispatcher.Instance().Enqueue(() =>
                {
                    try
                    {
                        // Désérialisation et interaction avec des objets Unity sur le thread principal
                        Message receivedMessage = JsonConvert.DeserializeObject<Message>(e.Data);
                        // try{
                        //     SystemMessage systemMessage = JsonConvert.DeserializeObject<SystemMessage>(receivedMessage.message);
                        //     Main.systemMessage.Add(systemMessage);
                        //     Debug.Log("Message système - Titre : " + systemMessage.GetTitre() + ", Code : " + systemMessage.GetCode());
                        // }
                        // catch {
                        //     Player.Instance.AddMessage(receivedMessage);  // Cette ligne doit être sur le thread principal
                        //     Debug.Log("Message Joueur 2 - Titre : " + receivedMessage.ToString());
                        // }
                        Player.Instance.AddMessage(receivedMessage);  // Cette ligne doit être sur le thread principal
                        Debug.Log("Message Joueur 2 - Titre : " + receivedMessage.ToString());
                        
                    }
                    catch (JsonException ex)
                    {
                        Debug.LogError("JSON deserialization error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Unexpected error: " + ex.Message);
                    }

                });
           
        };
        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket error: " + e.Message);
        };
        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket connection closed. erreur : " + e.Code);
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
