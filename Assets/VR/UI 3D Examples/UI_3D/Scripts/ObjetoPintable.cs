using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoPintable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Texturas"))
        {
            // Verifica si el collider tiene el tag "Texturas"

            // Encuentra el TextureManager y el CanvasTxtManager en la escena
            TextureManager textureManager = FindObjectOfType<TextureManager>();
            CanvasTxtManager canvasTxtManager = FindObjectOfType<CanvasTxtManager>();

            // Asegúrate de que el TextureManager y el CanvasTxtManager fueron encontrados
            if (textureManager != null && canvasTxtManager != null)
            {
                // Agrega este objeto a las listas de texturas en ambos managers
                canvasTxtManager.texturas.Add(this.gameObject);
                textureManager.texturas.Add(this.gameObject);

                // Llama al método AgregarTextura usando la referencia al objeto canvasTxtManager
                canvasTxtManager.AgregarTextura(this.gameObject);

                // También puedes desactivar o destruir este objeto si lo deseas
                gameObject.SetActive(false); // Desactiva el objeto
                // Destroy(gameObject); // Destruye el objeto
            }
            else
            {
                // Manejo de error o mensaje de advertencia si no se encuentran los managers
                Debug.LogError("TextureManager o CanvasTxtManager no encontrados en la escena.");
            }
        }
        else if (other.CompareTag("Basura"))
        {
            // Verifica si el collider tiene el tag "Basura"

            // Destruye este objeto
            Destroy(gameObject);
        }
    }
}

