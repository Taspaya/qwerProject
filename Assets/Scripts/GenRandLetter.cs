using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenRandLetter : MonoBehaviour {


    // TECLES DEL TECLAT 
    [SerializeField]
    public GameObject Q;
    public GameObject W;
    public GameObject E;
    public GameObject R;
    public GameObject D;
    public GameObject F;
    public GameObject uno;
    public GameObject dos;

    public Image sliderColor;                 // Slider color for feedback
    public GameObject lvlchanger;             // imagen de transicion para canviar de nivel
    public GameObject inGaMenu;               // game menu  

    public GameObject[] Tecles;
    // Time variables
    public float lifeTime;             // Variable tiempo ""vida""
    public float keyActiveTime;        // Variable tiempo que esta activa la tecla
    public float lvlupTime;            // Variable tiempo que tardara en desaparecer la imagen de cambio de nivel

    // Variables
    public float posX, posY;           // Variable x e y random que tendrà la letra activa
    public int nTecles;                // Variable numero de teclas activas (segun la dificultad)      
    public int a;                      // Variable numero de la tecla que se generara aleatoriamente
    public int score = 0;              // Variable que indica el numero de aciertos
    public int level = 1;              // Variable que rige el nivel de juego
    public float LastScore = 0.4f;     // Variable que rige la magnitud de la barra de vida inicial
    public int vides;                  
    public bool generated;             // Variable Bool que controla si hay alguna tecla generada en la pantalla en el momento
    public bool paused = false;        // Variable for pausing the game
  
    public Text lvlText;               // Texto que nos informa del nivel al que estamos
  

    [SerializeField]                    
    public Slider progressBar;
   

    // ============================================== START =========================================
    void Start () {
        nTecles = 4;                                                    // El juego comienza con solo 4 teclas, QWER;
        Tecles = new GameObject[8] { Q, W, E, R , D, F, uno, dos};      // Array que contiene las teclas que se van a generar
        generated = false;
        lvlupTime = 0;
        inGaMenu.SetActive(false);

        lvlText.text = "Lvl: " + level;                                 // Inicialicamos el texto del nivel en el "HUD"

        sliderColor.GetComponent<Image>().color = new Color32(50, 50, 50, 255); // Inicialicamos la imagen del slider en negro
        progressBar.value = lifeTime;                                   // Igualamos la longitud del slider con la barra de vida
        progressBar.maxValue = LastScore;                               // Inicialicamos la magnitud de la barra del slider               
    }

    // --------------------------------------------------- UPDATE ----------------------------------------------
    void Update()
    {

        if (paused) {                                                  // Si el juego esta pausado, solo se 
            Time.timeScale = 0;
            inGaMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                paused = !paused;
                Time.timeScale = 1;
                inGaMenu.SetActive(false);
            }
        }
        else { 
        
        lifeTime -= Time.deltaTime;                                      // Decrementamos en el tiempo la "barra de vida"
        progressBar.value = lifeTime;                                   // Assignamos el valor de la vida en la barra
        updateUI();                                                     // actualicamos el hud del nivel
        set_a_getKey();                                                 // Generamos letra y posiciones aleatorias


        setTimeToZero();                                             // Nos aseguramos que la barra de vida no baja de 0

        if (lifeTime >= LastScore)  // Si la score aconseguida es mayor o igual a la score maxima:
        {
            lvlUP();                // en caso de subir de nivel se ejecuta
            lvlupTime = 0;
            while (lvlupTime < 20000)
            {
                lvlupTime += Time.deltaTime;
            }
            lvlchanger.SetActive(false);                 // activamos la imagen de transación de nivel
        }

        if (level == 3)                                                  // Augmentamos la dificultad
        {
            nTecles = 6;
        }
    }
    }


    // ============================================ FUNCIONES ==============================================
    void lvlUP() {
      
            sliderColor.GetComponent<Image>().color = new Color32(50, 50, 50, 255); // Inicialicamos la imagen del slider en negro
            lifeTime = 0;                               // resetea el tiempo a 0
            LastScore = maxScoreCalc();                 // Calcula la nueva magnitud de la score maxima
            progressBar.maxValue = LastScore;           // assigna esta nueva magnitud al slider
            level++;                                    // augmenta en 1 el nivel          
    }

    void setTimeToZero()
    {
        if (lifeTime <= 0)
        {
            lifeTime = 0;
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

        if (Input.GetKeyDown(KeyCode.Escape)) {

            paused = !paused;

        }
        else if (generated && Input.GetKeyDown(KeyCode.Q) && a == 0)
        {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.W) && a == 1)
        {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.E) && a == 2)
        {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.R) && a == 3)
        {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.F) && a == 5) {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.D) && a == 4)
        {
            score++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.anyKeyDown)
        {
            vides--;
            lifeTime--;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(191, 0, 0, 255);

        }
    }
}
