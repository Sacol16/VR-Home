using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    public Vector3 localAxis;
    private Vector3 initialLocalPos;
    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    public float resertSpeed = 5;
    private bool freeze = false;
    [SerializeField] private ParticleSystem horneau;
    public Image imagen;
    float anchoActual;
    float aumento = 3.4f;
    float nuevoAncho;
    float anchoMaximo = 88.3f;
    public bool horneado = false;
    [SerializeField] private AudioSource bombasound;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
        anchoActual = imagen.rectTransform.sizeDelta.x;
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;
            Cambio();
            bombasound.Play();
            freeze = false;
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    void Update()
    {
        if (freeze)
            return;

        if (isFollowing)
        {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resertSpeed);
        }
    }
    public void Cambio()
    {
        if (anchoActual < anchoMaximo)
        {
        horneau.Play();
        anchoActual = imagen.rectTransform.sizeDelta.x;
        nuevoAncho = anchoActual + aumento;
        Vector2 pivot = imagen.rectTransform.pivot;
        pivot.x = 0;
        imagen.rectTransform.pivot = pivot;
        imagen.rectTransform.sizeDelta = new Vector2(nuevoAncho,imagen.rectTransform.sizeDelta.y);
        }
        else if(anchoActual >= anchoMaximo){
            horneado = true;
        }
}
}
