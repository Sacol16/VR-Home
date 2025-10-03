using UnityEngine;

public class WallMaterialChanger : MonoBehaviour
{
    private GameObject[] targetWalls; // Paredes específicas para cambiar

    public Material[] materials; // Arreglo de materiales

    public void SetTargetWalls(GameObject[] newTargetWalls)
    {
        targetWalls = newTargetWalls;
    }

    // Método para cambiar el material de las paredes objetivo
    public void ChangeWallMaterial(int index)
    {
        if (index < 0 || index >= materials.Length)
        {
            Debug.LogError("Índice de material fuera de rango.");
            return;
        }

        Material newMaterial = materials[index];

        foreach (GameObject wall in targetWalls)
        {
            if (wall != null)
            {
                Renderer wallRenderer = wall.GetComponent<Renderer>();
                if (wallRenderer != null)
                {
                    wallRenderer.material = newMaterial;
                }
            }
        }
    }
}

