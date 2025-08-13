using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;

    [Header("Disparo")]
    public float fuerzaDisparo = 10f;
    public float retroceso = 5f;
    public float tiempoEntreDisparos = 0.2f;
    private float tiempoUltimoDisparo = 0f;

    [Header("Aceleración y Desaceleración")]
    public float aceleracionTierra = 10f;
    public float aceleracionAire = 3f;
    public float desaceleracionTierra = 8f;
    public float desaceleracionAire = 2f;

    [Header("Referencias")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Animator animator;

    [Header("Respawn")]
    private Vector3 puntoInicio;

    private Rigidbody2D rb;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puntoInicio = transform.position;
    }

    void Update()
    {
        // Detectar si está en el suelo
        enSuelo = Mathf.Abs(rb.linearVelocity.y) < 0.01f;

        // Entrada horizontal
        float input = Input.GetAxisRaw("Horizontal");
        float velX = rb.linearVelocity.x;
        float factorMasa = 1f / rb.mass;

        if (input != 0)
        {
            float aceleracion = (enSuelo ? aceleracionTierra : aceleracionAire) * factorMasa;
            velX = Mathf.MoveTowards(velX, input * velocidad, aceleracion * Time.deltaTime);
        }
        else
        {
            float desaceleracion = (enSuelo ? desaceleracionTierra : desaceleracionAire) * factorMasa;
            velX = Mathf.MoveTowards(velX, 0, desaceleracion * Time.deltaTime);
        }

        rb.linearVelocity = new Vector2(velX, rb.linearVelocity.y);

        // Animaciones
        if (animator != null)
            animator.SetFloat("movement", Mathf.Abs(rb.linearVelocity.x));

        // Obtener posición del mouse en el mundo (para cámara ortográfica)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(
        new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)
    );
        mouseWorldPos.z = 0f;

        // Flip del personaje
        if (mouseWorldPos.x < transform.position.x)
            transform.localScale = new Vector3(-0.51386f, 0.51386f, 0.51386f);
        else
            transform.localScale = new Vector3(0.51386f, 0.51386f, 0.51386f);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        // Disparo
        if (Input.GetMouseButtonDown(0) && Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Disparar(mouseWorldPos);
            tiempoUltimoDisparo = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("KillZone"))
            Respawn();
    }

    void Respawn()
    {
        transform.position = puntoInicio;
        rb.linearVelocity = Vector2.zero;
    }

    void Disparar(Vector3 mouseWorldPos)
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            GameObject bala = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

            if (rbBala != null)
            {
                rbBala.gravityScale = 1f;

                // Dirección hacia el mouse
                Vector2 direccion = (mouseWorldPos - puntoDisparo.position);
                direccion.Normalize();

                rbBala.linearVelocity = direccion * fuerzaDisparo;

                // Retroceso al disparar
                rb.AddForce(-direccion * retroceso, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogWarning("El proyectil no tiene Rigidbody2D");
            }
        }
        else
        {
            Debug.LogWarning("ProyectilPrefab o puntoDisparo no asignados en el Inspector");
        }
    }
}
