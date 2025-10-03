using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flotacion : MonoBehaviour
{
    public float floatStrength = 1.0f;
    public float rotateSpeedX = 10.0f;
    public float rotateSpeedY = 5.0f;
    public float rotateSpeedZ = 7.0f;

    void FixedUpdate()
    {
        // Aplicar una fuerza hacia arriba para simular la flotación
        GetComponent<Rigidbody>().AddForce(Vector3.up * floatStrength);

        // Rotar el objeto alrededor de sus ejes locales
        transform.Rotate(Vector3.right * rotateSpeedX * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * rotateSpeedY * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * rotateSpeedZ * Time.deltaTime, Space.Self);
    }
}