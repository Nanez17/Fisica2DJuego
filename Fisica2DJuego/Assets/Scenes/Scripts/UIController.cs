using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerController player;
    public Slider sliderVelocidad;
    public Slider sliderSalto;
    public Slider sliderDisparo;

    void Start()
    {
        // Asignar valores iniciales al slider según el jugador
        sliderVelocidad.value = player.velocidad;
        sliderSalto.value = player.fuerzaSalto;
        sliderDisparo.value = player.velocidadDisparo;

        // Cuando el usuario mueva el slider, se actualiza la variable del jugador
        sliderVelocidad.onValueChanged.AddListener((v) => player.velocidad = v);
        sliderSalto.onValueChanged.AddListener((v) => player.fuerzaSalto = v);
        sliderDisparo.onValueChanged.AddListener((v) => player.velocidadDisparo = v);
    }
}



