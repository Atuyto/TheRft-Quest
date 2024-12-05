using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrushTrigger : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;
    public Material orangeMaterial;
    public Material yellowMaterial;
    public Material purpleMaterial;
    public Material pinkMaterial;
    public AudioSource splash;

    private Renderer targetRenderer;
    private Material originalMaterial;

    public List<GameObject> canvasSections;   
    public List<Material> expectedMaterials;  

    void Start()
    {
        targetRenderer = this.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            originalMaterial = targetRenderer.material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetRenderer != null)
        {
            if (other.CompareTag("RedColor"))
            {
                if (redMaterial != null)
                {
                    targetRenderer.material = redMaterial;
                }
            }
            else if (other.CompareTag("BlueColor"))
            {
                if (blueMaterial != null)
                {
                    targetRenderer.material = blueMaterial;
                }
            }
            else if (other.CompareTag("GreenColor"))
            {
                if (greenMaterial != null)
                {
                    targetRenderer.material = greenMaterial;
                }
            }
            else if (other.CompareTag("OrangeColor"))
            {
                if (orangeMaterial != null)
                {
                    targetRenderer.material = orangeMaterial;
                }
            }
            else if (other.CompareTag("YellowColor"))
            {
                if (yellowMaterial != null)
                {
                    targetRenderer.material = yellowMaterial;
                }
            }
            else if (other.CompareTag("PurpleColor"))
            {
                if (purpleMaterial != null)
                {
                    targetRenderer.material = purpleMaterial;
                }
            }
            else if (other.CompareTag("PinkColor"))
            {
                if (pinkMaterial != null)
                {
                    targetRenderer.material = pinkMaterial;
                }
            }
            else if (other.CompareTag("Canva"))
            {
                Renderer canvaRenderer = other.GetComponent<Renderer>();
                if (canvaRenderer != null)
                {
                    splash.Play();
                    canvaRenderer.material = targetRenderer.material;
                    CheckIfAllZonesFilled();
                }
            }
        }
    }

    void CheckIfAllZonesFilled()
    {
        bool allFilledCorrectly = true;

        for (int i = 0; i < canvasSections.Count; i++)
        {
            Renderer zoneRenderer = canvasSections[i].GetComponent<Renderer>();
            if (zoneRenderer != null)
            {
                Material zoneMaterial = zoneRenderer.material;
                Material expectedMaterial = expectedMaterials[i];

                if (zoneMaterial.color != expectedMaterial.color)
                {
                    allFilledCorrectly = false;
                    break; 
                }
            }
        }

        if (allFilledCorrectly)
        {
            Debug.Log("Toutes les zones sont remplies correctement !");
            StartCoroutine(WaitAndLoadScene(2f, "End"));
        }
        else
        {
            Debug.Log("Certaines zones ne sont pas remplies correctement.");
        }
    }

    private IEnumerator WaitAndLoadScene(float delay, string sceneName)
    {

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);

    }
}
