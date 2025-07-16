using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    public float velocidadDisparo = 10f;
    public float anguloDisparo = 0f;
    public float retroceso = 5f;

    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float movimiento = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y); // ✅ CORREGIDO
        animator.SetFloat("movement", Mathf.Abs(movimiento * velocidad));

        // Voltear personaje según dirección
        if (movimiento < 0)
        {
            transform.localScale = new Vector3(-0.51386f, 0.51386f, 0.51386f);
        }
        else if (movimiento > 0)
        {
            transform.localScale = new Vector3(0.51386f, 0.51386f, 0.51386f);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        // Disparo con tecla K
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject bala = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

            // Disparo en ángulo
            Vector2 direccion = Quaternion.Euler(0, 0, anguloDisparo) * Vector2.right;
            direccion.Normalize();

            rbBala.linearVelocity = direccion * velocidadDisparo;

            // Aplicar retroceso al personaje
            rb.AddForce(-direccion * retroceso, ForceMode2D.Impulse);
        }
    }
}
