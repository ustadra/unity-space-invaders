using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlAlien : MonoBehaviour
{
    // Conexión al marcador, para poder actualizarlo
    private GameObject marcador;
    private GameObject vidas;
    
    // Por defecto, 100 puntos por cada alien
    private int puntos = 100;

    // Objeto para reproducir la explosión de un alien
    private GameObject efectoExplosion;

    // Use this for initialization
    void Start()
    {
        // Localizamos el objeto que contiene el marcador
        if (marcador == null) {
             marcador = GameObject.Find("Marcador");
        }
        marcador.GetComponent<ControlMarcador>().vida = Vidas.contador;
        // Objeto para reproducir la explosión de un alien
        efectoExplosion = GameObject.Find("EfectoExplosion");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Detectar la colisión entre el alien y otros elementos

        // Necesitamos saber contra qué hemos chocado
        if (coll.gameObject.tag == "disparoenemigo")
        {
            Destroy(coll.gameObject);
        }
            if (coll.gameObject.tag == "disparo")
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                SceneManager.LoadScene("Nivel1");
                Vidas.puntos = 0;
            }
            // Sonido de explosión
            GetComponent<AudioSource>().Play();

            // Sumar la puntuación al marcador
            Vidas.puntos += puntos;
            marcador.GetComponent<ControlMarcador>().puntos = Vidas.puntos;

            // El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
            Destroy(coll.gameObject);

            // El alien desaparece (no hace falta retraso para la explosión, está en otro objeto)
            efectoExplosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);

        }
        else if (coll.gameObject.tag == "nave")
        {
            GetComponent<AudioSource>().Play();

            
            // El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
            Destroy(coll.gameObject);
            Vidas.contador = Vidas.contador - 1;
            marcador.GetComponent<ControlMarcador>().vida = Vidas.contador;
            if (Vidas.contador == 0)
            {
                SceneManager.LoadScene("Menu");
                Vidas.contador = 3;
                
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }



        }
    }
   
}