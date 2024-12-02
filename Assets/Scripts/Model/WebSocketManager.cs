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
    //private string url = "wss://lamb-master-vulture.ngrok-free.app/ws?idpersonne=1";
    private string url = "w&s://10.6.5.93:9001/ws?idpersonne=1";
    private Player player;
    private Coroutine pingCoroutine;
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
            if (pingCoroutine == null)
            {
                pingCoroutine = StartCoroutine(PingCoroutine());
            }
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
                        if (receivedMessage.from == "0" || receivedMessage.to == "0")
                        {
                            return;
                        }
                        //Debug.Log("Message Joueur 2 : " + receivedMessage.ToString());
                        try
                        {
                            SystemMessage systemMessage = JsonConvert.DeserializeObject<SystemMessage>(receivedMessage.message);
                            Main.systemMessage.Add(systemMessage);
                            Debug.Log("Message système - Titre : " + systemMessage.ToString());
                        }
                        catch
                        {
                            Player.Instance.AddMessage(receivedMessage);  // Cette ligne doit être sur le thread principal
                            Debug.Log("Message Joueur 2 - Titre : " + receivedMessage.ToString());
                        }
                        //Player.Instance.AddMessage(receivedMessage);  // Cette ligne doit être sur le thread principal
                        //Debug.Log("Message Joueur 2 - Titre : " + receivedMessage.ToString());

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

    private IEnumerator PingCoroutine()
    {
        while (ws != null && ws.IsAlive)
        {
            yield return new WaitForSeconds(30); // Attend 30 secondes entre chaque ping

            if (ws != null && ws.IsAlive)
            {
                Message message = new Message("Ping", "0", "0");
                ws.Send(JsonConvert.SerializeObject(message)); // Envoie un ping ou un message keep-alive
                Debug.Log("Ping envoyé");
            }
            else
            {
                Debug.LogWarning("Impossible d'envoyer le ping, la connexion WebSocket est fermée.");
            }
        }
    }

    void OnApplicationQuit()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }

        if (pingCoroutine != null)
        {
            StopCoroutine(pingCoroutine);
        }
    }

    public void SendMessage(string message)
    {
        if (ws != null && ws.IsAlive)
        {
        
            ws.Send(message);
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
