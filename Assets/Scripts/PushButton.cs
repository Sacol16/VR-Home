using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButton : MonoBehaviour
{
    public Animation puerta;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRSimpleInteractable>().selectEntered.AddListener(x => AbrirPuerta());
    }

    public void AbrirPuerta()
    {
        puerta.Play();
    }
}
