using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float gameTime;
    [SerializeField] TextMeshProUGUI timetextbox;

    private bool timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        // Puedes iniciar el temporizador aquí si lo deseas desde el principio
        // StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // Solo actualiza el tiempo si el temporizador ha comenzado
        if (timerStarted)
        {
            UpdateGameTime();
        }
    }

    private void UpdateGameTime()
    {
        gameTime -= Time.deltaTime;
        var minutes = Mathf.FloorToInt(gameTime / 60);
        var seconds = Mathf.FloorToInt(gameTime - minutes * 60);
        string gameTimeClockDisplay = string.Format("{0:0}:{1:00}", minutes, seconds);
        timetextbox.text = gameTimeClockDisplay;

        // Verificar si gameTime es igual o menor a cero
        if (gameTime <= 0)
        {
            // Cambiar el color del texto a rojo
            timetextbox.color = Color.red;

            // Puedes agregar más lógica aquí si es necesario cuando el tiempo llega a cero.
            // Por ejemplo, mostrar un mensaje de game over, detener el juego, etc.
        }
    }

    // Función para comenzar el temporizador
    public void StartTimer()
    {
        timerStarted = true;
    }
}
