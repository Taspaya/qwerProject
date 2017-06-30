using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenRandLetter : MonoBehaviour {

    [SerializeField]
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject D;
    public GameObject F;
    public GameObject uno;
    public GameObject dos;

    [SerializeField]
    public Canvas canvasito;

    public Image imagensita;            // Slider color for feedback

    public GameObject[] Tecles;

    public float lifeTime;             // Variable tiempo ""vida""
    public float keyActiveTime;        // Variable tiempo que esta activa la tecla
    public float posX, posY;           // Variable x e y random que tendrà la letra activa
    public int nTecles;                // Variable numero de teclas activas (segun la dificultad)      
    public int a;                      // Variable numero de la tecla que se generara aleatoriamente
    public int score = 0;              // Variable que indica el numero de aciertos
    public int level = 1;              // Variable que rige el nivel de juego
    public float LastScore = 0.4f;     // Variable que rige la magnitud de la barra de vida inicial
    public int vides;                  
    public bool generated;             // Variable Bool que controla si hay alguna tecla generada en la pantalla en el momento
 
  
    public Text lvlText;               // Texto que nos informa del nivel al que estamos
  

    [SerializeField]                    
    public Slider progressBar;
   

    // ============================================== START =========================================
    void Start () {
        nTecles = 4;                                                    // El juego comienza con solo 4 teclas, QWER;
        Tecles = new GameObject[8] { Q, W, E, R , D, F, uno, dos};      // Array que contiene las teclas que se van a generar

        vides = 5;                                                      
        generated = false;
    
        lvlText.text = "Lvl: " + level; 
       
        progressBar.value = lifeTime;                                   // Igualamos la longitud del slider con la barra de vida
        progressBar.maxValue = LastScore;
    }
	
	// --------------------------------------------------- UPDATE ----------------------------------------------
	void Update () {


        lifeTime -= Time.deltaTime;
        progressBar.value = lifeTime;
        updateUI();
        set_a_getKey();



        if (lifeTime <= 0)
        {
            lifeTime = 0;
        }

        if (lifeTime >= LastScore) {
            lifeTime = 0;
            LastScore = maxScoreCalc();
            progressBar.maxValue = LastScore;
            level++;
            
        }

        if(level == 3)
        {
            nTecles = 6;
        }

    }


    float maxScoreCalc() {

        if (level <= 3) {
            return LastScore + (LastScore * 0.2f);
        }
        else
        {

            return LastScore + (LastScore * 0.05f);
        }
       
    }

    void updateUI() {

        lvlText.text = "Lvl: " + level;
     

    }

    void generateRandomA() { 

        if (!generated)
        {
            for (int i = 0; i < 8; i++)  // DESACTIVEM LES TECLES AL GENERAR UNA TECLA NOVA
            {

                Tecles[i].SetActive(false);
            }

            a = Random.Range(0, nTecles);
            posX = Random.Range(-8, 8);
            posY = Random.Range(0, -7.3f);
            generated = true;
           
            Tecles[a].SetActive(true);
            Tecles[a].transform.position = new Vector2(posX, posY);
        }






    }
    void set_a_getKey() {

        generateRandomA();  // we generate random x & y and letter "a" 
     
        if (generated && Input.GetKeyDown(KeyCode.Q) && a == 0)
        {
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.W) && a == 1)
        {
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.E) && a == 2)
        {
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.R) && a == 3)
        {
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.F) && a == 5) {
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.D) && a == 4)
        { 
            score++;
            lifeTime++;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.anyKeyDown)
        {
            vides--;
            lifeTime--;
            generated = false;
            imagensita.GetComponent<Image>().color = new Color32(191, 0, 0, 255);

        }
    }
}
