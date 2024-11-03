using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BombaContador : MonoBehaviour
{
    float contador=5;
  
    public GameObject Explosion; 

    
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;


    // Update is called once per frame
    void Update()
    {
        contador -= Time.deltaTime; 
        if (contador<=0)
        {
            AutoDestroy();
            Golpe();
        }

    }


    

    void AutoDestroy ()
    {
        if ( Explosion !=null )
        {
            
            Instantiate( Explosion,transform.position,transform.rotation);

        }
       
        Destroy (gameObject);

    }

     private void Golpe()
    {
       

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy") ||colisionador.CompareTag("Player") )
            {
               // colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
                Debug.Log(" Te ha estallado ");
               
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
