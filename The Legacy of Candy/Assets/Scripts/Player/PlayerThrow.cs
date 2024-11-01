using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [Header("Bomba")]
    public GameObject bombaPrefab; // Prefab de la bomba que se lanzará
    public Transform puntoDeLanzamiento; // Punto desde donde se lanzará la bomba
    public float fuerzaDeLanzamiento = 10f; // Fuerza con la que se lanza la bomba

    void Update()
    {
        // Detecta si se presiona la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LanzarBomba(); // Llama al método para lanzar la bomba
        }
    }

    private void LanzarBomba()
    {
        // Crea una instancia de la bomba en la posición y rotación del punto de lanzamiento
        GameObject bomba = Instantiate(bombaPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);

        // Obtiene el Rigidbody2D de la bomba para aplicar una fuerza
        Rigidbody2D rbBomba = bomba.GetComponent<Rigidbody2D>();

        // Aplica la fuerza en la dirección en la que el personaje está mirando
        float direccion = transform.localScale.x > 0 ? 1f : -1f; // Determina la dirección en base a la escala del personaje
        rbBomba.AddForce(new Vector2(direccion * fuerzaDeLanzamiento, fuerzaDeLanzamiento), ForceMode2D.Impulse);
    }
}