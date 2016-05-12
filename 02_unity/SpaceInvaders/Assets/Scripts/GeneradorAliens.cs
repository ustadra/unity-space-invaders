using UnityEngine;
using System.Collections;

public class GeneradorAliens : MonoBehaviour
{
    Rigidbody2D[,] alienarray = new Rigidbody2D[4,7];
	// Publicamos la variable para conectarla desde el editor
	public Rigidbody2D prefabAlien1;
    // Velocidad a la que se desplaza la nave (medido en u/s)
    private float velocidad = 3f;
    private float velocidad2 =10f;
    private string aux = "centroizquierda";

    // Fuerza de lanzamiento del disparo
    private float fuerza = 0.5f;

    // Use this for initialization
    void Start ()
	{
		// Rejilla de 4x7 aliens
		generarAliens (4, 7, 1.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	// Calculamos la anchura visible de la cámara en pantalla
        float distanciaHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

        // Calculamos el límite izquierdo y el derecho de la pantalla
        float limiteIzq = -1.0f * distanciaHorizontal;
        float limiteDer = 1.0f * distanciaHorizontal;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {

                if (alienarray[i, j].transform.position.x > limiteIzq && aux == "centroizquierda")
                {
                    alienarray[i, j].transform.Translate(Vector2.left * velocidad * Time.deltaTime);
                }
                if (alienarray[i, j].transform.position.x < limiteIzq)
                {

                    alienarray[i, j].transform.Translate(Vector2.down * velocidad2 * Time.deltaTime);
                    alienarray[i, j].transform.Translate(Vector2.right * velocidad * Time.deltaTime);
                    velocidad2 = velocidad2 * 5;
                    aux = "centroderecha";
                }
                if (alienarray[i, j].transform.position.x < limiteDer && aux == "centroderecha")

                {
                    alienarray[i, j].transform.Translate(Vector2.right * velocidad * Time.deltaTime);
                }
                    if (alienarray[i, j].transform.position.x < limiteDer)
                {

                }
                    if (aux == "izquierda")
                {
                    alienarray[i, j].transform.Translate(Vector2.down * (velocidad * 10) * Time.deltaTime);
                    aux = "centroderecha";
                }
            }
        }
    }

	void generarAliens (int filas, int columnas, float espacioH, float espacioV, float escala = 1.0f)
	{
		/* Creamos una rejilla de aliens a partir del punto de origen
		 * 
		 * Ejemplo (2,5):
		 *   A A A A A
		 *   A A O A A
		 */

		// Calculamos el punto de origen de la rejilla
		Vector2 origen = new Vector2 (transform.position.x - (columnas / 2.0f) * espacioH + (espacioH / 2), transform.position.y);

		// Generamos los objetos
		for (int i = 0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				// Posición de cada alien
				Vector2 posicion = new Vector2 (origen.x + (espacioH * j), origen.y + (espacioV * i));

				// Instanciamos el objeto partiendo del prefab
				Rigidbody2D alien = (Rigidbody2D)Instantiate (prefabAlien1, posicion, transform.rotation);
                
				// Escala opcional, por defecto 1.0f (sin escala)
				// Nota: El prefab original ya está escalado a 0.2f
				alien.transform.localScale = new Vector2 (0.2f * escala, 0.2f * escala);
                alienarray[i,j] = alien;

            }
		}

	}

}
