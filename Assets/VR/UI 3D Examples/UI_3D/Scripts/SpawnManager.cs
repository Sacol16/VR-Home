using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class PrefabData
    {
        public GameObject prefab;
        public bool used;
    }

    public List<PrefabData> prefabsToSpawn = new List<PrefabData>();
    public Transform[] spawnPoints;
    public float spawnInterval = 5.0f;
    public GameObject completado;
    public bool finalizado = false;
    //public float initialDelay = 7.0f;

    private List<int> usedIndexes = new List<int>();

    private TextureManager textureManager;
    private RotateObjectWithKnob rotateObject;
    private bool spawnStarted = false;

    private void Start()
    {
        // Encuentra la instancia de TextureManager
        textureManager = FindObjectOfType<TextureManager>();

        // Encuentra la instancia de RotateObjectWithKnob
        rotateObject = FindObjectOfType<RotateObjectWithKnob>();

        
    }

    private void Update()
{
    
    if (rotateObject.estaAgarrado && !spawnStarted)
    {
        InvokeRepeating("SpawnPrefab", 0.0f, spawnInterval);
        spawnStarted = true;
    }
}



    private void SpawnPrefab()
    {
        if (prefabsToSpawn.Count == 0 || spawnPoints.Length == 0)
        {
            

            // Si prefabsToSpawn está vacía, asigna aleatoriamente los materiales desde TextureManager
            if (textureManager != null)
            {
               // textureManager.AssignRandomMaterials();
                completado.SetActive(true);
                finalizado = true;
            }

            
        }

        List<int> availableIndexes = GetAvailableIndexes();

        if (availableIndexes.Count > 0)
        {
            int randomIndex = availableIndexes[Random.Range(0, availableIndexes.Count)];
            int randomSpawnPointIndex = GetRandomSpawnPointIndex();

            if (randomSpawnPointIndex != -1)
            {
                GameObject newPrefab = Instantiate(prefabsToSpawn[randomIndex].prefab, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);

                // Marca como utilizado en la lista temporal
                prefabsToSpawn[randomIndex].used = true;

                // Elimina el objeto usado de la lista
                prefabsToSpawn.RemoveAt(randomIndex);
            }
        }

        // Si todos los prefabs se han utilizado, reinicia las listas
        if (prefabsToSpawn.Count == 0)
        {
            usedIndexes.Clear();
        }
    }

    private List<int> GetAvailableIndexes()
    {
        List<int> availableIndexes = new List<int>();

        for (int i = 0; i < prefabsToSpawn.Count; i++)
        {
            if (!usedIndexes.Contains(i) && !prefabsToSpawn[i].used)
            {
                availableIndexes.Add(i);
            }
        }

        return availableIndexes;
    }

    private int GetRandomSpawnPointIndex()
    {
        return Random.Range(0, spawnPoints.Length);
    }

    // Método para establecer la referencia a TextureManager
    public void SetTextureManager(TextureManager tm)
    {
        textureManager = tm;
    }
}
