using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CircleSlider : MonoBehaviour
{
    [SerializeField] Transform handle;
    [SerializeField] Image fill;
    [SerializeField] Text valTxt;
    [SerializeField] XRRayInteractor rightHandRay;

    public Light[] lights; // Ajusta esta variable en el inspector para incluir todas las luces que deseas controlar

    // Intensidad mínima y máxima para las luces
    public float minIntensity = 0f;
    public float maxIntensity = 2f;

    private bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            Vector3 pointerPosition;
            if (TryGetPointerPosition(out pointerPosition))
            {
                UpdateHandle(pointerPosition);
                UpdateLightsIntensity();
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

    public void onHandleDrag()
    {
        if (isDragging)
        {
            Vector3 pointerPosition;
            if (TryGetPointerPosition(out pointerPosition))
            {
                UpdateHandle(pointerPosition);
                UpdateLightsIntensity();
            }
        }
    }

    private bool TryGetPointerPosition(out Vector3 pointerPosition)
    {
        pointerPosition = Vector3.zero;

        if (rightHandRay.TryGetCurrent3DRaycastHit(out RaycastHit rightHit))
        {
            pointerPosition = rightHit.point;
            return true;
        }

        return false;
    }

    private void UpdateHandle(Vector3 pointerPosition)
    {
        Vector2 dir = pointerPosition - handle.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;

        if (angle <= 225 || angle >= 315)
        {
            Quaternion r = Quaternion.AngleAxis(angle + 135f, Vector3.forward);
            handle.localRotation = Quaternion.Euler(0, 0, r.eulerAngles.z);
            angle = ((angle >= 315) ? (angle - 360) : angle) + 45;
            fill.fillAmount = 0.75f - (angle / 360f);
            valTxt.text = Mathf.Round((fill.fillAmount * 100) / 0.75f).ToString();
        }
    }

    private void UpdateLightsIntensity()
    {
        float angle = GetHandleRotationAngle();
        float intensity = Mathf.Pow(angle / 180f, 2) * maxIntensity;
        intensity = Mathf.Clamp(intensity, 0f, maxIntensity); // Limita la intensidad a un valor máximo de 2

        foreach (Light light in lights)
        {
            if (light != null)
            {
                light.intensity = intensity;
            }
        }
    }




    // Esta función devuelve el ángulo de rotación actual del manillar
    private float GetHandleRotationAngle()
    {
        float currentAngle = handle.localRotation.eulerAngles.z;
        if (currentAngle > 180f)
        {
            currentAngle -= 360f;
        }
        return currentAngle + 45f; // Offset de 45 grados para que el ángulo esté en el rango correcto
    }
}
