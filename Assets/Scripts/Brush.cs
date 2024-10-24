using UnityEngine;

public class BrushTrigger : MonoBehaviour
{
    public Material redMaterial; 
    public Material blueMaterial; 
    public Material greenMaterial;
    public Material orangeMaterial;
    public Material yellowMaterial;
    public Material purpleMaterial;
    public Material pinkMaterial;

    private Renderer targetRenderer;
    private Material originalMaterial;

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
                    canvaRenderer.material = targetRenderer.material;
                }
            }
        }
    }
}
