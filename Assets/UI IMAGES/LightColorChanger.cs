using UnityEngine;

public class LightColorChanger : MonoBehaviour
{
    public Light[] lights; // Ajusta esta variable en el inspector para incluir todas las luces que deseas controlar
    public Color targetColor = Color.white; // Color al que deseas cambiar las luces

    public void ChangeLightColor()
    {
        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.color = targetColor;
            }
        }
    }
}
