using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public WallMaterialChanger wallMaterialChanger;
    public GameObject[] targetWalls; // Paredes específicas para esta habitación

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wallMaterialChanger.SetTargetWalls(targetWalls);
        }
    }
}
