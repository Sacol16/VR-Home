using UnityEngine;
using UnityEngine.UI;

public class MaterialChangerButton : MonoBehaviour
{
    public WallMaterialChanger wallMaterialChanger;
    public Button[] changeMaterialButtons; // Arreglo de botones

    private void Start()
    {
        if (changeMaterialButtons != null && wallMaterialChanger != null)
        {
            for (int i = 0; i < changeMaterialButtons.Length; i++)
            {
                int index = i; // Captura el índice actual
                changeMaterialButtons[index].onClick.AddListener(() => wallMaterialChanger.ChangeWallMaterial(index));
            }
        }
    }
}


