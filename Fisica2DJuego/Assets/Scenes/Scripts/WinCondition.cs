using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinCondition : MonoBehaviour
{
    public GameObject winPanel; // Asignar en el Inspector
    public TMP_Text winText; // Asignar en el Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winPanel.SetActive(true);
            winText.text = "¡Ganaste!\nPuntos: " + ScoreManager.instance.score;

            Time.timeScale = 0f; // Pausa el juego
        }
    }
}