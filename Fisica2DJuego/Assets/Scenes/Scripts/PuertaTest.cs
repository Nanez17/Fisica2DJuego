using UnityEngine;

public class PuertaTest : MonoBehaviour
{
    public GameObject puertaTilemap; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            AbrirPuerta();
        }
    }

    void AbrirPuerta()
    {
        if (puertaTilemap != null)
        {
            puertaTilemap.SetActive(false); 
            Debug.Log("Puerta abierta");
        }
        else
        {
            Debug.LogWarning("No se asignó la puerta en el Inspector");
        }
    }
}
