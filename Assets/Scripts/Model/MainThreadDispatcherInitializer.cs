using UnityEngine;

public class MainThreadDispatcherInitializer : MonoBehaviour
{
    void Awake()
    {
        if (MainThreadDispatcher.Instance() == null)
        {
            Debug.LogError("Failed to create MainThreadDispatcher!");
        }
        else
        {
            Debug.Log("MainThreadDispatcher created successfully.");
        }
    }
}
