using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePuerta : MonoBehaviour
{
    public GameObject puertaTilemap; 

    public void AbrirPuerta()
    {
        if (puertaTilemap != null)
        {
            puertaTilemap.SetActive(false); 
            Debug.Log("Puerta abierta");
        }
    }
}
