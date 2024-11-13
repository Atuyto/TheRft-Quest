using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class LightTrigger : MonoBehaviour
{
    public GameObject light;
    public Material newMaterial;
    public Material originalMaterial;
    public Material loseMaterial;
    public Material winMaterial;
    public AudioSource audio;
    public AudioSource right;
    public AudioSource wrong;
    private Renderer renderer;
    private bool isCoroutineRunning = false;
    private bool won = false;

    private bool isInTrigger = false;

    public static List<string> playerSequence = new List<string>();
    public string lightID;
    public static List<LightTrigger> allLights = new List<LightTrigger>();

    public static List<string> correctSequence = new List<string> { "Red", "Green", "Blue" };

    void Start()
    {
        if (light != null)
        {
            renderer = light.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterial = renderer.material;
            }
        }

        allLights.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCoroutineRunning && !won && !isInTrigger)
        {
            isInTrigger = true;

            if (light != null && newMaterial != null)
            {
                renderer.material = newMaterial;

                playerSequence.Add(lightID);

                if (!CheckSequence())
                {
                    Debug.Log("S�quence incorrecte ! Vous avez perdu.");
                    wrong.Play();
                    StartCoroutine(ChangeAllLightsToRed());
                    playerSequence.Clear();
                }
                else if (playerSequence.Count == correctSequence.Count)
                {
                    Debug.Log("S�quence correcte !");
                    won = true;
                    right.Play();
                    ChangeAllLightsToGreen();
                    playerSequence.Clear();

                    WebSocketManager webSocketManager = FindObjectOfType<WebSocketManager>();
                    SystemMessage systemMessage = new SystemMessage("Oculus", "12502");
                    Message message = new Message(JsonConvert.SerializeObject(systemMessage), "1", "2");
                    webSocketManager.SendMessage(JsonConvert.SerializeObject(message));
                    SceneManager.LoadScene("SceneFree");
                }
                else
                {
                    audio.Play();
                }

                StartCoroutine(ResetTriggerDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isCoroutineRunning && !won)
        {
            if (light != null && newMaterial != null)
            {
                renderer.material = originalMaterial;
            }
        }
    }

    private bool CheckSequence()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    // Coroutine pour changer toutes les lumi�res en rouge
    private IEnumerator ChangeAllLightsToRed()
    {
        isCoroutineRunning = true;

        for (int i = 0; i < 4; i++)
        {
            foreach (LightTrigger lightTrigger in allLights)
            {
                lightTrigger.renderer.material = loseMaterial;
            }

            yield return new WaitForSeconds(0.5f);

            foreach (LightTrigger lightTrigger in allLights)
            {
                lightTrigger.renderer.material = lightTrigger.originalMaterial;
            }

            yield return new WaitForSeconds(0.3f);
        }

        isCoroutineRunning = false;
    }

    // Coroutine pour r�initialiser le trigger apr�s un d�lai
    private IEnumerator ResetTriggerDelay()
    {
        yield return new WaitForSeconds(1f);
        isInTrigger = false; 
    }

    private void ChangeAllLightsToGreen()
    {
        foreach (LightTrigger lightTrigger in allLights)
        {
            lightTrigger.renderer.material = winMaterial;
        }
    }
}
