using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    public List<GameObject> objetosParaPintar = new List<GameObject>();
    public List<GameObject> texturas = new List<GameObject>();
    private bool pintado = false;
   
    void Start()
    {
        SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
        if (spawnManager != null)
        {
            spawnManager.SetTextureManager(this); // Asigna esta instancia de TextureManager a SpawnManager
        }
    }

    // Método para asignar aleatoriamente los materiales a los objetos de objetosParaPintar
    public void AssignRandomMaterials()
    {
        if(!pintado){
        foreach (GameObject objetoParaPintar in objetosParaPintar)
        {
            Renderer renderer = objetoParaPintar.GetComponent<Renderer>();

            if (renderer != null && texturas.Count > 0)
            {
                int randomTextureIndex = Random.Range(0, texturas.Count);
                renderer.material = new Material(texturas[randomTextureIndex].GetComponent<Renderer>().material);
                
            }
            pintado = true;
        }
        }
    }
}
