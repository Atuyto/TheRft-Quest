using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private string name;
    private List<Message> messages;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Player");
                instance = go.AddComponent<Player>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            messages = new List<Message>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<Message> GetMessages()
    {
        return messages;
    }

    public void AddMessage(Message message)
    {
        messages.Add(message);
    }
}
