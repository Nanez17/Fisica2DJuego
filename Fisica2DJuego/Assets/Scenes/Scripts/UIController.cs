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
    public Slider sliderFuerzaDisparo; 
    public Slider sliderAngulo;
    public Slider sliderAire;
    public Slider sliderRetroceso;

    // Textos para mostrar valores actuales
    public TMP_Text valorGravedad;
    public TMP_Text valorMasa;
    public TMP_Text valorFriccion;
    public TMP_Text valorSalto;
    public TMP_Text valorFuerzaDisparo; 
    public TMP_Text valorAngulo;
    public TMP_Text valorAire;
    public TMP_Text valorRetroceso;




    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.SetParent(transform.root, false); 
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.anchoredPosition = new Vector2(10, -10);
        rt.localScale = new Vector3(0.7f, 0.7f, 1f);


        // Asignar valores iniciales
        sliderGravedad.value = Physics2D.gravity.y;
        sliderMasa.value = player.GetComponent<Rigidbody2D>().mass;
        sliderFriccion.value = GetFriction();
        sliderSalto.value = player.fuerzaSalto;

        sliderAire.value = player.GetComponent<Rigidbody2D>().linearDamping;
        sliderRetroceso.value = player.retroceso;

        // Listeners
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

        sliderFuerzaDisparo.onValueChanged.AddListener((v) => { 
            player.fuerzaDisparo = v; 
            valorFuerzaDisparo.text = v.ToString("F2");
        });

        sliderAire.onValueChanged.AddListener((v) => {
            player.GetComponent<Rigidbody2D>().linearDamping = v;
            valorAire.text = v.ToString("F2");
        });

        sliderRetroceso.onValueChanged.AddListener((v) => {
            player.retroceso = v;
            valorRetroceso.text = v.ToString("F2");
        });

        // Textos iniciales
        valorGravedad.text = sliderGravedad.value.ToString("F2");
        valorMasa.text = sliderMasa.value.ToString("F2");
        valorFriccion.text = sliderFriccion.value.ToString("F2");
        valorSalto.text = sliderSalto.value.ToString("F2");
        valorFuerzaDisparo.text = sliderFuerzaDisparo.value.ToString("F2");
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
