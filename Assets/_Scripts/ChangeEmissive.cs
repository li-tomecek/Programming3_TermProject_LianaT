using UnityEngine;
using UnityEngine.Events;


/*
 * An example emissive-changing class from UI/UX
 * 
 */
public class ChangeEmissive : MonoBehaviour
{
    public GameObject textMesh;
    public MeshRenderer screenRenderer;
    public Material emissiveMaterial;
    public Material normalMaterial;

    private bool isOn = false;

    public UnityEvent Light = new UnityEvent();


    private void Start()
    {
        TurnOffComputer();
    }

    private void OnMouseDown()
    {
        if (isOn)
            TurnOffComputer();
        else
            TurnOnComputer();
    }


    //To change an emissive material, we copy the materials array, change it, and set it again 

    private void TurnOnComputer()
    {
        if (screenRenderer != null && emissiveMaterial != null)
        {

            Material[] mats = screenRenderer.materials; //Get copy of the materials list array 
            mats[1] = emissiveMaterial; //Desired emissive material (glowing screen) 
            screenRenderer.materials = mats;     // Override the original reference with our changed copy 
        }

        if (textMesh != null)
            textMesh.SetActive(true);

        isOn = true;
    }

    private void TurnOffComputer()
    {
        if (screenRenderer != null && normalMaterial != null)
        {
            Material[] mats = screenRenderer.materials; //Get copy of the materials list array 
            mats[1] = normalMaterial; //Desired material (screen off) 
            screenRenderer.materials = mats;  // Override the original reference with our changed copy 
        }

        if (textMesh != null)
            textMesh.SetActive(false);

        isOn = false;
    }

    private void ChangeEmissiveTo(Color color)
    {
        emissiveMaterial.SetColor("_EmissionColor", color);
    }

}


