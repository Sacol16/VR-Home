using System.Threading;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DisparoRay : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 25;

    public LineRenderer lineRenderer;

    public GameObject impact;

    public AudioSource DisparoSonido;

    public void Disparar()
    {
        StartCoroutine(Disparo());
    }

    IEnumerator Disparo()
    {
        RaycastHit hit;
        bool hitInfo = Physics.Raycast(firePoint.position, firePoint.right, out hit, 50f);
        DisparoSonido.Play();

        if (hitInfo)
        {
            TiroAlBlanco tiro = hit.transform.GetComponent<TiroAlBlanco>();
            
            
            if (tiro != null)
            {
                if (hit.transform.tag == "Aceptar")
                {
                    tiro.aceptado = true;
                    Debug.Log("Etiqueta 'Aceptar' detectada.");
                }
                else if (hit.transform.tag == "Descartar")
                {
                    tiro.descartado = true;
                    Debug.Log("Etiqueta Descartar detectada. ");
                }
                else
                {
                    tiro.acertado = true;
                    Debug.Log("Etiqueta 'Aceptar' detectada. tiro.acertado = true");

                }
            }

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);

            Instantiate(impact, hit.point, Quaternion.identity);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * 20);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}