using UnityEngine;
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
            // Si l'objet est d�j� snapp� le maintenir en place
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
                Debug.Log($"Piece {i} is not in the correct snap zone.");
                break;
            }
        }

        if (allCorrect)
        {
            // Si toutes les pi�ces sont � leur place faire dispara�tre le plateau
            //DisappearPuzzle();
            WebSocketManager webSocketManager = FindObjectOfType<WebSocketManager>();
            SystemMessage systemMessage = new SystemMessage("Oculus", "12501");
            Message message = new Message(JsonConvert.SerializeObject(systemMessage), "1", "2");
            webSocketManager.SendMessage(JsonConvert.SerializeObject(message));
            SceneManager.LoadScene("SceneFree");
        }
    }

    void DisappearPuzzle()
    {
        // Faire dispara�tre le plateau
        Debug.Log("Puzzle completed! Plateau disappeared.");
        puzzleBase.gameObject.SetActive(false);
        foreach (Transform piece in puzzlePieces)
        {
            piece.gameObject.SetActive(false);
        }
        puzzleBaseDone.gameObject.SetActive(true);
    }
}
