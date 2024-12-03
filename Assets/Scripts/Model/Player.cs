using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    private string name;
    public ObservableList<Message> messages;

    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            messages = new ObservableList<Message>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
