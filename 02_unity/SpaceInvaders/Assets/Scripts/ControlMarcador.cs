using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class ControlMarcador : MonoBehaviour
{

	// Puntos ganados en la partida
	public int puntos = 0;
    public int vida = 3;
    // Objeto donde mostramos el texto
    public GameObject puntuacion;
    public GameObject vidas;
    private Text t;
    private Text v;

    // Use this for initialization
    void Start ()
	{
		// Localizamos el componente del UI
		t = puntuacion.GetComponent<Text> ();
        v = vidas.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		// Actualizamos el marcador
		t.text = SceneManager.GetActiveScene().name+"   Puntos: " + puntos.ToString () + "\n";
        v.text = "Vidas: " + vida.ToString() + "\n";
    }

}
