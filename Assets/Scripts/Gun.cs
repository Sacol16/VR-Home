using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    public XRGrabInteractable grabInteract;
    public Transform shootPoint;

    public DisparoRay disparo;

    public GameObject shootFX;

    // Start is called before the first frame update
    void Start()
    {
        grabInteract.activated.AddListener(x => Disparando());
        grabInteract.deactivated.AddListener(x => DejarDisparo());
    }

    public void Disparando()
    {
        Instantiate(shootFX, shootPoint.position, shootPoint.rotation);
        disparo.Disparar();
    }

    public void DejarDisparo()
    {
        
    }
}
