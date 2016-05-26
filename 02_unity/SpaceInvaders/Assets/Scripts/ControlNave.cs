using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlNave : MonoBehaviour
{

	// Velocidad a la que se desplaza la nave (medido en u/s)
	private float velocidad = 20f;
    private float velocidadalien = 10f;
    // Fuerza de lanzamiento del disparo
    private float fuerza = 0.5f;
    private int contadordisparos = 0;
    private int aliendisparo = 10;
	// Acceso al prefab del disparo
	public Rigidbody2D disparo;
    public Rigidbody2D objeto;
    private GameObject efectoExplosion;
    private GameObject marcador;
    // Use this for initialization
    void Start ()
    {
        efectoExplosion = GameObject.Find("EfectoExplosion");
        if (marcador == null)
        {
            marcador = GameObject.Find("Marcador");
        }
        marcador.GetComponent<ControlMarcador>().vida = Vidas.contador;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "disparoenemigo")
        {
            
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





            // El disparo desaparece (cuidado, si tiene eventos no se ejecutan)
            Destroy(coll.gameObject);

            // El alien desaparece (no hace falta retraso para la explosión, está en otro objeto)
            efectoExplosion.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
           

        }
    }
    // Update is called once per frame
        void Update ()
	{
		// Calculamos la anchura visible de la cámara en pantalla
		float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

		// Calculamos el límite izquierdo y el derecho de la pantalla
		float limiteIzq = -1.0f * distanciaHorizontal;
		float limiteDer = 1.0f * distanciaHorizontal;
        if (objeto.gameObject.tag == "nave") { 
		// Tecla: Izquierda
		if (Input.GetKey (KeyCode.LeftArrow)) {

			// Nos movemos a la izquierda hasta llegar al límite para entrar por el otro lado
			if (transform.position.x > limiteIzq) {
				transform.Translate (Vector2.left * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteDer, transform.position.y);			
			}
		}

		// Tecla: Derecha
		if (Input.GetKey (KeyCode.RightArrow)) {

			// Nos movemos a la derecha hasta llegar al límite para entrar por el otro lado
			if (transform.position.x < limiteDer) {
				transform.Translate (Vector2.right * velocidad * Time.deltaTime);
			} else {
				transform.position = new Vector2 (limiteIzq, transform.position.y);			
			}
		}

		// Disparo
		if (Input.GetKeyDown (KeyCode.Space)) {
			disparar ();
                
		}
        }
        if (objeto.gameObject.tag == "alien")
        {
           
            if (transform.position.x > limiteIzq)
            {
                transform.Translate(Vector2.left * velocidadalien * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(limiteDer, transform.position.y);
            }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                disparar2();

            }

        
    }

	void disparar ()
	{
		// Hacemos copias del prefab del disparo y las lanzamos
		Rigidbody2D d = (Rigidbody2D)Instantiate (disparo, transform.position, transform.rotation);

		// Desactivar la gravedad para este objeto, si no, ¡se cae!
		d.gravityScale = 0;

		// Posición de partida, en la punta de la nave
		d.transform.Translate (Vector2.up * 0.7f);

		// Lanzarlo
		d.AddForce (Vector2.up * fuerza, ForceMode2D.Impulse);	
	}
    void disparar2()
    {
        // Hacemos copias del prefab del disparo y las lanzamos
        Rigidbody2D d = (Rigidbody2D)Instantiate(disparo, transform.position, transform.rotation);

        // Desactivar la gravedad para este objeto, si no, ¡se cae!
        d.gravityScale = 0;

        // Posición de partida, en la punta de la nave
        d.transform.Translate(Vector2.down * 0.7f);

        // Lanzarlo
        d.AddForce(Vector2.down * fuerza, ForceMode2D.Impulse);
    }
}
