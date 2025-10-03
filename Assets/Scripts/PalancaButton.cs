using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PalancaButton : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    Collider buttonCollider; // Agrega una referencia al collider del botón

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;

        // Obtén el collider del botón
        buttonCollider = button.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0, 0.003f);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
        else // Segundo toque: Baja el botón
        {
            button.transform.localPosition = new Vector3(0, 0, 0.015f);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    // Actualizado para que el collider se mueva con el objeto que sube
    private void OnTriggerStay(Collider other)
    {
        if (isPressed && other.gameObject == presser)
        {
            // Mueve el collider junto con el objeto que sube
            buttonCollider.transform.position = button.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            // Restaura la posición del collider al salir del área
            buttonCollider.transform.localPosition = Vector3.zero;
        }
    }
}
