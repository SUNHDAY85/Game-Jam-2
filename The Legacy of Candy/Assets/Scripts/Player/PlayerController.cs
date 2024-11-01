using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D; // Referencia al Rigidbody2D para aplicar físicas al jugador

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f; // Almacena el valor de movimiento horizontal
    [SerializeField] private float velocidadDeMovimiento; // Velocidad del jugador
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento; // Suavizado del cambio de dirección
    private Vector3 velocidad = Vector3.zero; // Almacena la velocidad actual para suavizar la transición
    private bool mirandoDerecha = true; // Indica si el jugador está mirando a la derecha

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto; // Fuerza que se aplica al jugador al saltar
    [SerializeField] private Transform controladorSuelo; // Posición del detector de suelo
    [SerializeField] private Vector3 dimensionesCaja; // Tamaño de la caja para la detección de suelo
    [SerializeField] private bool enSuelo; // Indica si el jugador está tocando el suelo
    private bool salto = false; // Indica si el jugador ha intentado saltar

    private void Start()
    {
        // Inicializa el Rigidbody2D al iniciar el juego
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Captura el movimiento horizontal basado en las teclas de dirección (izquierda/derecha)
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

        // Detecta si se ha presionado la flecha hacia arriba para saltar
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            salto = true; // Se activa la acción de salto
        }
    }

    private void FixedUpdate()
    {
        // Llama al método Mover, pasando el movimiento horizontal y si el jugador intentó saltar
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        // Reinicia la variable de salto para evitar saltos continuos
        salto = false;
    }

    // Método para mover al jugador
    private void Mover(float mover, bool saltar)
    {
        // Calcula la velocidad objetivo del jugador en función de la dirección y velocidad de movimiento
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        
        // Aplica suavizado en la transición de velocidad para evitar cambios bruscos
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        // Gira al personaje si se mueve en la dirección opuesta a donde está mirando
        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        // Salta si el jugador está en el suelo y ha intentado saltar
        if (enSuelo && saltar)
        {
            enSuelo = false; // Desactiva enSuelo para evitar múltiples saltos hasta volver al suelo
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto), ForceMode2D.Impulse); // Aplica la fuerza de salto
        }
    }

    // Método que detecta cuando el jugador toca el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto con el que colisiona tiene el tag "Floor", significa que está en el suelo
        if (collision.gameObject.CompareTag("Floor"))
        {
            enSuelo = true; // Establece enSuelo como true para permitir saltar nuevamente
        }
    }

    // Método que detecta cuando el jugador deja de tocar el suelo
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si el objeto con el que deja de colisionar tiene el tag "Floor", significa que ya no está en el suelo
        if (collision.gameObject.CompareTag("Floor"))
        {
            enSuelo = false; // Establece enSuelo como false para evitar saltar mientras está en el aire
        }
    }

    // Método para girar al personaje en la dirección opuesta
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha; // Cambia la dirección de mirada del jugador
        Vector3 escala = transform.localScale; // Obtiene la escala actual del jugador
        escala.x *= -1; // Invierte la escala en el eje X para cambiar la dirección
        transform.localScale = escala; // Asigna la nueva escala al jugador
    }
}
