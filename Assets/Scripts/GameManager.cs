using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TextureManager textureManager;
    public ColorManager colorManager;
    public ButtonFollowVisual buttonFollowVisual;
    //public GameObject zapato;
    public Animator animatorZapato;
    public AudioSource deslizar;
    public AudioSource ambiente;
    public AudioSource final;
    public GameObject CanvaFinal;
    
    
    private bool finalizoPintura = false, finalizoTextura = false, finalizoHorno = false;


    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        colorManager = FindObjectOfType<ColorManager>();
        textureManager = FindObjectOfType<TextureManager>();
        buttonFollowVisual = FindObjectOfType<ButtonFollowVisual>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnManager.finalizado && colorManager.terminado && buttonFollowVisual.horneado){
            textureManager.AssignRandomMaterials();
            colorManager.AsignarColor();
            CanvaFinal.SetActive(true);
            final.Play();
        }

        if(colorManager.terminado && !finalizoPintura){
            finalizoPintura = true;
            animatorZapato.SetTrigger("Pintura");
            deslizar.Play();
        }

         if(spawnManager.finalizado && !finalizoTextura){
            finalizoTextura = true;
            animatorZapato.SetTrigger("Textura");
            deslizar.Play();
        }

          if(buttonFollowVisual.horneado && !finalizoHorno){
            finalizoHorno = true;
            animatorZapato.SetTrigger("Bomba");
            deslizar.Play();
        }
    }

    public void MusicaAmbiente(){
        ambiente.Play();
    }

    public void RecargarEscena()
    {
        
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;    
        SceneManager.LoadScene(indiceEscenaActual);
    }

}