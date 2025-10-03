using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public WallMaterialChanger wallMaterialChanger;
    public GameObject[] targetWalls; // Paredes espec�ficas para esta habitaci�n

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wallMaterialChanger.SetTargetWalls(targetWalls);
        }
    }
}
