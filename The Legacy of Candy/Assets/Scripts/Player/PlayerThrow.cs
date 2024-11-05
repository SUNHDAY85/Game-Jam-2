using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [Header("Bomba")]
    public GameObject bombaPrefab; // Prefab de la bomba que se lanzará
    public Transform puntoDeLanzamiento; // Punto desde donde se lanzará la bomba
    public float fuerzaDeLanzamiento = 10f; // Fuerza con la que se lanza la bomba
  
    public float timeThrow=0.3f;

    public float  timePress=0f;// la dejo publica para ver en el unity 

    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    public AudioClip attackAudioClip;

    void Update()
    {


         if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        // Detecta si se esta  presiona la barra espaciadora y determina el tiempo de presion 
        if (Input.GetKey(KeyCode.Space) /*&& tiempoSiguienteAtaque <= 0*/) 
        {
           timePress+=Time.deltaTime; 
           //tiempoSiguienteAtaque = tiempoEntreAtaques;
           
        }
        if (Input.GetKeyUp(KeyCode.Space)&& tiempoSiguienteAtaque <= 0) //  se deja de presionar la barra 
        {
            if (timePress >= timeThrow) // si el tiempo de presionado es mayor al rango se lanza la bomba 
            {
                LanzarBomba(); 
            }
            else
            {
                ColocarBomba(); // lllamado de funcion que coloca bomba en el mismo sitio 
            }
            AudioManager.instance.PlaySFX(attackAudioClip);

            tiempoSiguienteAtaque = tiempoEntreAtaques;
            timePress = 0f; // Reinicia el temporizador
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

    private void ColocarBomba(){

        Instantiate(bombaPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);
    }
}