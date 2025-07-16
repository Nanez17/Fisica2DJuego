using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerController player;

    // Sliders
    public Slider sliderGravedad;
    public Slider sliderMasa;
    public Slider sliderFriccion;
    public Slider sliderSalto;
    public Slider sliderVelDisparo;
    public Slider sliderAngulo;
    public Slider sliderAire;
    public Slider sliderRetroceso;

    // Textos para mostrar valores actuales
    public TMP_Text valorGravedad;
    public TMP_Text valorMasa;
    public TMP_Text valorFriccion;
    public TMP_Text valorSalto;
    public TMP_Text valorVelDisparo;
    public TMP_Text valorAngulo;
    public TMP_Text valorAire;
    public TMP_Text valorRetroceso;

    void Start()
    {
        Debug.Log("Player velocidad inicial: " + player.velocidad);
        Debug.Log("SliderGravedad antes: " + sliderGravedad.value);

        // Ejemplo de prueba
        sliderGravedad.value = -9.8f;
        Debug.Log("SliderGravedad después: " + sliderGravedad.value);
        // Asignar valores iniciales
        sliderGravedad.value = Physics2D.gravity.y;
        sliderMasa.value = player.GetComponent<Rigidbody2D>().mass;
        sliderFriccion.value = GetFriction();
        sliderSalto.value = player.fuerzaSalto;
        sliderVelDisparo.value = player.velocidadDisparo;
        sliderAngulo.value = player.anguloDisparo;
        sliderAire.value = player.GetComponent<Rigidbody2D>().linearDamping;
        sliderRetroceso.value = player.retroceso;

        // Asignar listeners con actualización de texto
        sliderGravedad.onValueChanged.AddListener((v) => {
            Physics2D.gravity = new Vector2(0, v);
            valorGravedad.text = v.ToString("F2");
        });

        sliderMasa.onValueChanged.AddListener((v) => {
            player.GetComponent<Rigidbody2D>().mass = v;
            valorMasa.text = v.ToString("F2");
        });

        sliderFriccion.onValueChanged.AddListener((v) => {
            SetFriction(v);
            valorFriccion.text = v.ToString("F2");
        });

        sliderSalto.onValueChanged.AddListener((v) => {
            player.fuerzaSalto = v;
            valorSalto.text = v.ToString("F2");
        });

        sliderVelDisparo.onValueChanged.AddListener((v) => {
            player.velocidadDisparo = v;
            valorVelDisparo.text = v.ToString("F2");
        });

        sliderAngulo.onValueChanged.AddListener((v) => {
            player.anguloDisparo = v;
            valorAngulo.text = v.ToString("F1") + "°";
        });

        sliderAire.onValueChanged.AddListener((v) => {
            player.GetComponent<Rigidbody2D>().linearDamping = v;
            valorAire.text = v.ToString("F2");
        });

        sliderRetroceso.onValueChanged.AddListener((v) => {
            player.retroceso = v;
            valorRetroceso.text = v.ToString("F2");
        });

        // Mostrar textos iniciales
        valorGravedad.text = sliderGravedad.value.ToString("F2");
        valorMasa.text = sliderMasa.value.ToString("F2");
        valorFriccion.text = sliderFriccion.value.ToString("F2");
        valorSalto.text = sliderSalto.value.ToString("F2");
        valorVelDisparo.text = sliderVelDisparo.value.ToString("F2");
        valorAngulo.text = sliderAngulo.value.ToString("F1") + "°";
        valorAire.text = sliderAire.value.ToString("F2");
        valorRetroceso.text = sliderRetroceso.value.ToString("F2");
    }

    float GetFriction()
    {
        Collider2D col = player.GetComponent<Collider2D>();
        return col.sharedMaterial != null ? col.sharedMaterial.friction : 0f;
    }

    void SetFriction(float v)
    {
        Collider2D col = player.GetComponent<Collider2D>();
        if (col.sharedMaterial != null)
            col.sharedMaterial.friction = v;
    }
}
