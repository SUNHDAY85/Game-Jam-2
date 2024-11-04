using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && tiempoSiguienteAtaque <= 0 )
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
            Debug.Log(" atacando");
            

        }else {

           // animator.SetBool("attack",false);

        }
    }

    private void Golpe()
    {
        animator.SetBool("attack",true);
        StartCoroutine( "apagar");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<PlayerExample>().TakeDamage();
                
            }
        }
         
        
    }
    IEnumerator apagar ()
    {

        yield return new WaitForSeconds( 0.6f);
        animator.SetBool("attack",false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
