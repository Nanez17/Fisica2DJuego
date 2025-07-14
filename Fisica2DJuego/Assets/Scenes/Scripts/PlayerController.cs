using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;
    private Rigidbody2D rb;
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float velocidadDisparo = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimiento horizontal
        float movimiento = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movimiento * velocidad, rb.linearVelocity.y);

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
            rbBala.linearVelocity = Vector2.right * velocidadDisparo;

            // Retroceso (opcional)
            rb.AddForce(Vector2.left * velocidadDisparo, ForceMode2D.Impulse);
        }
    }
}

