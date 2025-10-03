using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COlOR : MonoBehaviour
{
    public float intervaloDeCambio = 2f; // Tiempo en segundos entre cambios de color

    void Start()
    {
        InvokeRepeating("CambiarColoresAleatorios", 0f, intervaloDeCambio);
    }

    void CambiarColoresAleatorios()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material material = renderer.material;
            material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}