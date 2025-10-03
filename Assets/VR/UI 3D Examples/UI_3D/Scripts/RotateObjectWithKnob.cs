using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Content.Interaction;

public class RotateObjectWithKnob : MonoBehaviour
{
    [SerializeField]
    private XRKnob knob;

    [SerializeField]
    private float rotationMultiplier = 0.3f;

    [SerializeField]
    private float maxRotationAngle = 30.0f;

    public bool estaAgarrado = false;

    private void Start()
    {
        // Establece la rotación inicial a 0 grados
        transform.rotation = Quaternion.Euler(0.0f, -90.0f, 30.0f);
    }

    private void OnEnable()
    {
        if (knob != null)
        {
            knob.onSelectEntered.AddListener(OnGrab);
            knob.onSelectExited.AddListener(OnRelease);
            knob.onValueChange.AddListener(RotateObject);
        }
        else
        {
            Debug.LogError("XRKnob reference not set in RotateObjectWithKnob script.");
        }
    }

    private void OnDisable()
    {
        if (knob != null)
        {
            knob.onSelectEntered.RemoveListener(OnGrab);
            knob.onSelectExited.RemoveListener(OnRelease);
            knob.onValueChange.RemoveListener(RotateObject);
        }
    }

    private void OnGrab(XRBaseInteractor interactor)
    {
        estaAgarrado = true;
    }

    private void OnRelease(XRBaseInteractor interactor)
    {
        estaAgarrado = false;
    }

    private void RotateObject(float knobValue)
    {
        if (estaAgarrado)
        {
            // Ajusta la rotación del objeto basándote en el valor del knob
            float newRotation = knobValue * 360.0f * rotationMultiplier;

            // Restringe la rotación en el eje Z a ±45 grados
            newRotation = Mathf.Clamp(newRotation, -maxRotationAngle, maxRotationAngle);

            // Aplica la rotación al objeto
            transform.rotation = Quaternion.Euler(180.0f, -90.0f, newRotation);
        }
    }
}
