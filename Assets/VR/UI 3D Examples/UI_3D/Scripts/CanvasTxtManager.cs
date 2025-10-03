using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTxtManager : MonoBehaviour
{
    public List<GameObject> objetosParaPintar = new List<GameObject>();
    public List<GameObject> texturas = new List<GameObject>();

    private int indexObjetoParaPintar = 0; // Índice del objetoParaPintar activo

    // Método para ejecutar cuando se agrega una textura
    public void AgregarTextura(GameObject textura)
    {
        // Asegúrate de que la textura no sea nula y tenga un componente Renderer
        Renderer texturaRenderer = textura.GetComponent<Renderer>();
        if (texturaRenderer != null)
        {
            // Obtén el material de la textura
            Material materialTextura = texturaRenderer.material;

            // Asegúrate de que haya al menos un objetoParaPintar
            if (objetosParaPintar.Count > 0)
            {
                // Activa el objetoParaPintar correspondiente al índice actual
                GameObject objetoParaPintar = objetosParaPintar[indexObjetoParaPintar];
                objetoParaPintar.SetActive(true);

                // Asegúrate de que el objeto tenga un componente Renderer
                Renderer objetoRenderer = objetoParaPintar.GetComponent<Renderer>();
                if (objetoRenderer != null)
                {
                    // Asigna el material al objetoParaPintar actual
                    objetoRenderer.material = materialTextura;

                    // Incrementa el índice para el próximo objetoParaPintar
                    indexObjetoParaPintar = (indexObjetoParaPintar + 1) % objetosParaPintar.Count;
                }
                else
                {
                    Debug.LogError("El objetoParaPintar no tiene un componente Renderer.");
                }
            }
            else
            {
                Debug.LogWarning("No hay objetosParaPintar configurados.");
            }
        }
        else
        {
            Debug.LogError("La textura no tiene un componente Renderer.");
        }
    }
}
