using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Radial_Selection : MonoBehaviour
{
    public OVRInput.Button handType;

    [Range(2,10)]
    public int numberOfRadialPart;

    private int numberOfIcons = 3;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart=10;
    public Transform handTransform;
    public Transform handTransform2;

    public Canvas[] icons;

    public UnityEvent<int> OnPartSelected;

    public List<GameObject> canvases = new List<GameObject>();

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }

        foreach (Canvas icon in icons)
        {
            icon.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(handType))
        {
            SpawnRadialPart();
        }
        if(OVRInput.Get(handType))
        {
            GetSelectedRadialPart();
        }
        if(OVRInput.GetUp(handType))
        {
            HideAndTriggerSelected();
        }
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);

        foreach (Canvas icon in icons)
        {
            icon.gameObject.SetActive(false);
        }

        foreach (var canvas in canvases)
        {
            canvas.gameObject.SetActive(false);
        }

        if( currentSelectedRadialPart != 2)
        {
            GameObject canvasToShow = canvases[currentSelectedRadialPart];
            canvasToShow.gameObject.SetActive(true);
        }
        else
        {
            foreach (var canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }
        }
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if(angle < 0)
        {
            angle += 360;
        }

        Debug.Log("ANGLE " + angle);

        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            if(i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = 1f * Vector3.one;
            }
        }

        foreach (Canvas icon in icons)
        {
            icon.gameObject.SetActive(false);
        }

        if (currentSelectedRadialPart >= 0 && currentSelectedRadialPart < icons.Length)
        {
            Canvas canvasToShow = icons[currentSelectedRadialPart];
            canvasToShow.gameObject.SetActive(true);
        }
    }

    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        for (int i = 0; i<numberOfIcons; i++)
        {
            Canvas canvasToShow = icons[i];
            canvasToShow.transform.position = handTransform.position;
        }

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }
        
        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = - i * 360 / numberOfRadialPart - angleBetweenPart/2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            spawnedParts.Add(spawnedRadialPart);
        }
    }
}
