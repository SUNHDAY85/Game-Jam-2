using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BombaContador : MonoBehaviour

{  
    public float contador;
    public GameObject Explosion;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float force;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    [Header("Audio")]
    public AudioClip bombAudioClip;

    private bool haExplotado = false; // Nueva variable para controlar la explosión

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        contador -= Time.deltaTime; // Disminuye la variable a medida que pasa el tiempo 
        if (contador <= 0 && !haExplotado) // Verifica que la explosión aún no haya ocurrido
        {
            haExplotado = true; // Marca que la explosión ocurrió
            AutoDestroy();
            Golpe();
        }
    }

    void AutoDestroy()
    {
        if (animator != null)
        {
            animator.SetBool("Explotion", true);
            StartCoroutine(EsperarFinAnimacion());
           // AudioManager.instance.PlaySFX(bombAudioClip);
        }
    }

    private IEnumerator EsperarFinAnimacion()
    {
        yield return null; // Espera un frame para que el Animator se actualice

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Explotion"))
        {
            yield return null; // Espera hasta que la animación comience
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null; // Espera a que la animación termine
        }

        Destroy(gameObject); // Destruye el objeto al final de la animación
    }

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
              Rigidbody2D rb2D =colisionador.GetComponent<Rigidbody2D>();

              if(rb2D !=null)
              {
                Vector2 direccion = colisionador.transform.position - transform.position;
                float distancia = 1 + direccion.magnitude;
                float   finalForce=force/distancia;
                rb2D.AddForce(direccion*finalForce);

              }

            if ( colisionador.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<PlayerLife>().TakeDamage();
                Debug.Log("Te ha estallado");
            }else  if (colisionador.CompareTag("Enemy") )
            {
                colisionador.transform.GetComponent<EnemyLife>().TakeDamage();
            }
            AppliedForce();
        }
    }

    private  void AppliedForce  ()
    {
      

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
