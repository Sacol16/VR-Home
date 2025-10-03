using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    [SerializeField] Transform handle;  // Objeto que representa la perilla
    [SerializeField] Image fill;       // Barra de progreso para el volumen
    [SerializeField] Text valTxt;      // Texto que muestra el valor de volumen
    [SerializeField] AudioSource audioSource;  // AudioSource a controlar
    public float maxVolume = 1f;       // Volumen máximo permitido

    public delegate void VolumeChanged(float newVolume);
    public event VolumeChanged OnVolumeChanged; // Evento para notificar cambios de volumen

    private bool isDragging = false; // Bandera para saber si la perilla está siendo arrastrada

    void Start()
    {
        // Inicializa el estado del handle y el volumen
        if (audioSource != null)
        {
            UpdateHandleBasedOnVolume(audioSource.volume);
        }
    }

    void Update()
    {
        // Actualiza la posición del handle mientras está siendo arrastrado
        if (isDragging)
        {
            Vector3 pointerPosition;
            if (TryGetPointerPosition(out pointerPosition))
            {
                UpdateHandle(pointerPosition);
                UpdateAudioVolume();
            }
        }
    }

    public void OnHandleGrab()
    {
        isDragging = true;
    }

    public void OnHandleRelease()
    {
        isDragging = false;
    }

    private bool TryGetPointerPosition(out Vector3 pointerPosition)
    {
        // Aquí puedes implementar la lógica para obtener la posición del puntero
        // En este ejemplo, simplemente inicializamos la posición en Vector3.zero.
        pointerPosition = Vector3.zero;

        // Si estás usando un raycast, agrega la lógica para detectar el hit del puntero.
        return true;
    }

    private void UpdateHandle(Vector3 pointerPosition)
    {
        // Calcula la dirección desde la perilla al puntero
        Vector2 direction = pointerPosition - handle.position;

        // Calcula el ángulo en grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ajusta el ángulo al rango correcto (360 grados)
        angle = (angle <= 0) ? (360 + angle) : angle;

        // Solo permite rotar la perilla dentro del rango [135°, 315°]
        if (angle >= 135f && angle <= 315f)
        {
            // Actualiza la rotación del handle
            Quaternion rotation = Quaternion.AngleAxis(angle - 135f, Vector3.forward);
            handle.localRotation = rotation;

            // Actualiza la barra de progreso y el texto
            float normalizedAngle = (angle - 135f) / 180f; // Normaliza al rango [0, 1]
            fill.fillAmount = normalizedAngle;
            valTxt.text = Mathf.Round(normalizedAngle * 100).ToString();
        }
    }

    private void UpdateAudioVolume()
    {
        // Obtiene el ángulo actual del handle
        float angle = GetHandleRotationAngle();

        // Convierte el ángulo en un valor de volumen normalizado
        float volume = Mathf.Clamp(angle / 180f * maxVolume, 0f, maxVolume);

        // Actualiza el volumen del AudioSource
        audioSource.volume = volume;

        // Notifica el cambio de volumen
        OnVolumeChanged?.Invoke(volume);

        // Actualiza el texto con el valor del volumen en porcentaje
        valTxt.text = Mathf.Round(volume * 100).ToString();
    }

    private float GetHandleRotationAngle()
    {
        // Obtiene la rotación en el eje Z del handle y la ajusta al rango correcto
        float currentAngle = handle.localRotation.eulerAngles.z;
        if (currentAngle > 180f)
        {
            currentAngle -= 360f;
        }
        return currentAngle + 45f; // Ajusta para que 0° corresponda al mínimo volumen
    }

    private void UpdateHandleBasedOnVolume(float volume)
    {
        // Calcula el ángulo basado en el volumen
        float angle = Mathf.Clamp(volume / maxVolume * 180f, 0f, 180f);
        Quaternion rotation = Quaternion.AngleAxis(angle - 135f, Vector3.forward);
        handle.localRotation = rotation;

        // Actualiza la barra de progreso y el texto
        fill.fillAmount = volume / maxVolume;
        valTxt.text = Mathf.Round(volume * 100).ToString();
    }
}
