using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class elevator : MonoBehaviour
{
    public XRLever elevador;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float upSpeed = speed * (elevador.value ? 1 : 0);
        Vector3 velocity = new Vector3(0, upSpeed, 0);
        transform.position += velocity * Time.deltaTime;
    }
}
