using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boton : MonoBehaviour
{

    [SerializeField] private float threshold = 0.9f;
    [SerializeField] private float deadZone = 11f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    public float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;        

        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        Debug.Log(value);

        return Mathf.Clamp(value, -1f, 1f);
    }

    public void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Presionado");
    }

    public void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Soltado");
    }
}
