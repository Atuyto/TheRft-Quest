using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class SnapObject : MonoBehaviour
{
    public List<Transform> snapZones;
    public List<Transform> puzzlePieces;
    public Transform puzzleBase;
    public GameObject puzzleBaseDone;
    private bool isSnapped = false;
    private Transform currentSnapZone;
    private static Dictionary<Transform, bool> snapZoneOccupied = new Dictionary<Transform, bool>();

    void Start()
    {
        foreach (Transform snapZone in snapZones)
        {
            snapZoneOccupied[snapZone] = false;
        }
    }

    void Update()
    {
        if (isSnapped)
        {
            // Si l'objet est déjà snapé, le maintenir en place
            transform.position = currentSnapZone.position;
            transform.rotation = currentSnapZone.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnapZone") && !isSnapped)
        {
            Transform closestSnapZone = GetClosestSnapZone(other.transform);
            if (closestSnapZone != null && !snapZoneOccupied[closestSnapZone])
            {
                snapZoneOccupied[closestSnapZone] = true;
                currentSnapZone = closestSnapZone;
                isSnapped = true;
                CheckPuzzleCompletion();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SnapZone") && isSnapped && other.transform == currentSnapZone)
        {
            // L'objet sort de la zone de snap
            snapZoneOccupied[currentSnapZone] = false;
            isSnapped = false;
            currentSnapZone = null;
        }
    }

    Transform GetClosestSnapZone(Transform snapZone)
    {
        Transform closestSnapZone = null;
        float closestDistance = float.MaxValue;

        foreach (Transform zone in snapZones)
        {
            float distance = Vector3.Distance(transform.position, zone.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSnapZone = zone;
            }
        }

        return closestSnapZone;
    }

    void CheckPuzzleCompletion()
    {
        bool allCorrect = true;

        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            SnapObject pieceScript = puzzlePieces[i].GetComponent<SnapObject>();
            if (pieceScript.currentSnapZone != snapZones[i])
            {
                allCorrect = false;
                Debug.Log($"Pièce {i} n'est pas dans la zone de snap correcte.");
                break;
            }
        }

        if (allCorrect)
        {
            // Si toutes les pièces sont à leur place, faire disparaître le plateau
            //DisappearPuzzle();
            WebSocketManager webSocketManager = FindObjectOfType<WebSocketManager>();
            SystemMessage systemMessage = new SystemMessage("Oculus", "12501");
            Message message = new Message(JsonConvert.SerializeObject(systemMessage), "1", "2");
            webSocketManager.SendMessage(JsonConvert.SerializeObject(message));
    
            // Lance la coroutine pour attendre avant de charger la scène
            StartCoroutine(WaitAndLoadScene(2f, "FreeScene"));
        }
    }

    void DisappearPuzzle()
    {
        // Faire disparaître le plateau
        Debug.Log("Puzzle terminé ! Le plateau a disparu.");
        puzzleBase.gameObject.SetActive(false);
        foreach (Transform piece in puzzlePieces)
        {
            piece.gameObject.SetActive(false);
        }
        puzzleBaseDone.gameObject.SetActive(true);
    }

    // Nouvelle coroutine pour attendre avant de charger la scène
    private IEnumerator WaitAndLoadScene(float delay, string sceneName)
    {
        
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }
}
