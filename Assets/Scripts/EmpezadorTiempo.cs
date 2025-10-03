using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpezadorTiempo : MonoBehaviour
{
    public Timer timer;

 

   public void OnCollisionEnter(Collision collision)
{
    timer.StartTimer();
}

}
