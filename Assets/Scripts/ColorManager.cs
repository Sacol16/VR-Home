using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color color; 
    public int tiro;
    public bool terminado; 
    
    public List<GameObject> componentesZapatos = new List<GameObject>(); 

    public GameObject cantidadPinturaRoja, cantidadPinturaVerde, cantidadPinturaAzul, cantidadPinturaBlanca ;

     Vector3 cantidadInicialRoja, posicionInicialPinturaRoja, cantidadInicialVerde, posicionInicialPinturaVerde,  
             cantidadInicialAzul, posicionInicialPinturaAzul, cantidadInicialBlanca, posicionInicialPinturaBlanca ; 

    // Start is called before the first frame update
    void Start()
    {
        tiro = 0;
        terminado = false; // Inicializar terminado como falso al inicio

        cantidadInicialRoja = cantidadPinturaRoja.transform.localScale; // Guarda la escala inicial
        posicionInicialPinturaRoja = cantidadPinturaRoja.transform.position; // Guarda la posición inicial

        cantidadInicialAzul = cantidadPinturaAzul.transform.localScale; // Guarda la escala inicial
        posicionInicialPinturaAzul = cantidadPinturaAzul.transform.position; // Guarda la posición inicial

        cantidadInicialVerde = cantidadPinturaVerde.transform.localScale; // Guarda la escala inicial
        posicionInicialPinturaVerde = cantidadPinturaVerde.transform.position; // Guarda la posición inicial

        cantidadInicialBlanca = cantidadPinturaBlanca.transform.localScale; // Guarda la escala inicial
        posicionInicialPinturaBlanca = cantidadPinturaBlanca.transform.position; // Guarda la posición inicial
    }

    public void Restaurar()
    {
        // Restaurar las escalas y posiciones iniciales de las pinturas
        cantidadPinturaRoja.transform.localScale = cantidadInicialRoja;
        cantidadPinturaRoja.transform.position = posicionInicialPinturaRoja;

        cantidadPinturaAzul.transform.localScale = cantidadInicialAzul;
        cantidadPinturaAzul.transform.position = posicionInicialPinturaAzul;

        cantidadPinturaVerde.transform.localScale = cantidadInicialVerde;
        cantidadPinturaVerde.transform.position = posicionInicialPinturaVerde;

        cantidadPinturaBlanca.transform.localScale = cantidadInicialBlanca;
        cantidadPinturaBlanca.transform.position = posicionInicialPinturaBlanca;

        Debug.Log("Se ejecutó la función de restaurar");
    }


    public void AsignarColor(){
        foreach (GameObject zapato in componentesZapatos)
            {
                Renderer renderer = zapato.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
    }
}
