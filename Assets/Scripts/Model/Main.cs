using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static string codeEnigme2 = "12501";
    public static string  codeEnigme3 = "12502";
    public static ObservableList<SystemMessage> systemMessage;
    public static Main Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            systemMessage = new ObservableList<SystemMessage>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
