using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Color = UnityEngine.Color;  // Agrega un alias a UnityEngine.Color


public class TiroAlBlanco : MonoBehaviour
{
    public bool acertado = false;
    public Image imagen;
    float anchoActual;
    float aumento = 3.4f;
    float nuevoAncho;
    float anchoMaximo = 17.0f;
    public ColorManager colorManager;


    public Color colorMezclado;

    public bool aceptado, descartado;

    public Text textCompletado;

  

    void Start()
    {
        anchoActual = imagen.rectTransform.sizeDelta.x;
        aceptado = false;
        descartado = false;
    }
    

    public void Cambio(Color colorElegido)
    {
        anchoActual = imagen.rectTransform.sizeDelta.x;

        if (anchoActual < anchoMaximo)
        {
            nuevoAncho = anchoActual + aumento;

            Vector2 pivot = imagen.rectTransform.pivot;
            pivot.x = 0;
            imagen.rectTransform.pivot = pivot;

            imagen.rectTransform.sizeDelta = new Vector2(nuevoAncho, imagen.rectTransform.sizeDelta.y);
            Vector3 reduccion = new Vector3(0.0f, 0.002f, 0.0f);

            if(colorElegido == Color.red)
            {  
                 colorManager.cantidadPinturaRoja.transform.localScale -= reduccion;
                 colorManager.cantidadPinturaRoja.transform.position -= colorManager.cantidadPinturaRoja.transform.up * reduccion.y * 20.0f;
            }
            if(colorElegido == Color.green)
            {  
                 colorManager.cantidadPinturaVerde.transform.localScale -= reduccion;
                 colorManager.cantidadPinturaVerde.transform.position -= colorManager.cantidadPinturaVerde.transform.up * reduccion.y * 20.0f;
            }
            if(colorElegido == Color.blue)
            {  
                 colorManager.cantidadPinturaAzul.transform.localScale -= reduccion;
                 colorManager.cantidadPinturaAzul.transform.position -= colorManager.cantidadPinturaAzul.transform.up * reduccion.y * 20.0f;
            }
            if(colorElegido == Color.white)
            {  
                 colorManager.cantidadPinturaBlanca.transform.localScale -= reduccion;
                 colorManager.cantidadPinturaBlanca.transform.position -= colorManager.cantidadPinturaBlanca.transform.up * reduccion.y * 20.0f;
            }
           


            if(colorManager.tiro < 2 && colorElegido != Color.white)
            {
                    colorMezclado = new Color(
                    colorElegido.r + colorManager.color.r,
                    colorElegido.g + colorManager.color.g,
                    colorElegido.b,
                    1.0f
                );
                Debug.Log(colorManager.color);
                Debug.Log(colorElegido);
           

            }

            else {

                colorMezclado = Color.Lerp(colorElegido, colorManager.color, 0.5f);
                Debug.Log(colorManager.color);
                Debug.Log(colorElegido);
               
            }


            //Color colorMezclado = Color.Lerp(colorElegido, colorManager.color, 0.5f);

            //Color colorMezclado = new Color(
            //    (colorElegido.r + colorManager.color.r) / 2.0f,
            //    (colorElegido.g + colorManager.color.g) / 2.0f,
            //    (colorElegido.b + colorManager.color.b) / 2.0f,
            //    (colorElegido.a + colorManager.color.a) / 2.0f
            //);
       
            colorManager.color = colorMezclado;
        

            // Descomenta y ajusta según sea necesario
            // sistemaDeParticulas.Play();
            // var mainModule = sistemaDeParticulas.main;
            // mainModule.startColor = colorManager.color;

            imagen.color = colorManager.color;
            acertado = false;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aceptado)
        {
            if (this.gameObject.tag == "Aceptar") 
            {
                colorManager.terminado = true; 
                imagen.rectTransform.sizeDelta = new Vector2(anchoMaximo, imagen.rectTransform.sizeDelta.y);
                textCompletado.text = "Completado";
                aceptado = false; 

            }
        
        }

        if(descartado)
        {
            if (this.gameObject.tag == "Descartar") 
            {
                colorManager.tiro = 0; 
                Debug.Log(colorManager.tiro);
                imagen.rectTransform.sizeDelta = new Vector2(anchoActual, imagen.rectTransform.sizeDelta.y);
                colorManager.Restaurar();
                Debug.Log("Se llamo a la funcion de restaurar");
                descartado = false; 
            }

        }

        if (acertado)
        {
            if (colorManager.tiro == 0)
            {
                if (this.gameObject.tag == "ColorRojo") {  colorManager.color = Color.red; Cambio(Color.red);}
                if (this.gameObject.tag == "ColorAzul") {  colorManager.color = Color.blue; Cambio(Color.blue);}
                if (this.gameObject.tag == "ColorBlanco") {  colorManager.color = Color.white; Cambio(Color.white); }
                if (this.gameObject.tag == "ColorVerde") {  colorManager.color = Color.green;Cambio(Color.green); }
                if (this.gameObject.tag == "ColorNegro") {  colorManager.color = Color.black;Cambio(Color.black); }
                colorManager.tiro += 1;
                Debug.Log("Entre a tiro cero");
                acertado = false;
            }
            else
            {
                if (this.gameObject.tag == "ColorRojo") {Cambio(Color.red);}
                if (this.gameObject.tag == "ColorAzul") {Cambio(Color.blue);}
                if (this.gameObject.tag == "ColorBlanco") {Cambio(Color.white);}
                if (this.gameObject.tag == "ColorVerde") {Cambio(Color.green);}
                if (this.gameObject.tag == "ColorNegro") {Cambio(Color.black); }
                colorManager.tiro += 1;
                Debug.Log("Entre tiro mayor a uno");
                acertado = false;
            }
        }

        //if(colorManager.tiro == 5)
        //{
        //    colorManager.terminado = true; 
        //     Debug.Log("Completaste el color");
        //    colorManager.tiro += 1;
        //}
    }

}
