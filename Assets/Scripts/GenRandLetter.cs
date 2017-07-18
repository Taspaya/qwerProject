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

    // game menu  
    public GameObject inGaMenu;               // game pad menu  
    public Button resumeBTN;                  // Return button
    public GameObject estadistiques;          // Statistics screen

    // Keys
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
    public bool onStatistics;
    public int encerts;
    public int errors;
    public int accuracy;  

    // texts
    public Text lvlText;               // Texto que nos informa del nivel al que estamos


    [SerializeField]                    
    public Slider progressBar;
   

    // ============================================== START =========================================
    void Start () {
        nTecles = 4;                                                    // El juego comienza con solo 4 teclas, QWER;
        Tecles = new GameObject[8] { Q, W, E, R , D, F, uno, dos};      // Array que contiene las teclas que se van a generar
        generated = false;
        lvlupTime = 0;
        inGaMenu.SetActive(false);                                      // Game pause starts at off mode
        estadistiques.SetActive(false);                                 // Statistics canvas starts at off mode
        lvlText.text = "Lvl: " + level;                                 // Inicialicamos el texto del nivel en el "HUD"
        encerts = 0;                                                    // Inicialicamos n de aciertos a 0
        errors = 0;                                                     // Inicialicamos n de errores a 0
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
            if(onStatistics && Input.GetKeyDown(KeyCode.Escape))
            {
                back_BTN();

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
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


        setTimeToZero();                                                // Nos aseguramos que la barra de vida no baja de 0

        if (lifeTime >= LastScore)                                      // Si la score aconseguida es mayor o igual a la score maxima:
        {
            lvlUP();                                                    // en caso de subir de nivel se ejecuta
            lvlupTime = 0;
            while (lvlupTime < 20000)
            {
                lvlupTime += Time.deltaTime;
            }
            lvlchanger.SetActive(false);//  **********                  // activamos la imagen de transación de nivel
        }

        if (level == 3)                                                  // Aumentamos la dificultad
        {
            nTecles = 6;
        }
    }
    }


               // ============================================ FUNCIONES ============================================== //
    void lvlUP() {
      
            sliderColor.GetComponent<Image>().color = new Color32(50, 50, 50, 255); // Inicialicamos la imagen del slider en negro
            lifeTime = 0;                               // resetea el tiempo a 0
            LastScore = maxScoreCalc();                 // Calcula la nueva magnitud de la score maxima
            progressBar.maxValue = LastScore;           // assigna esta nueva magnitud al slider
            level++;                                    // augmenta en 1 el nivel          
    }


    // BUTTON EVENTS
    public void resume_BTN() {                          // If the resume button of the pause canvas is pressed...   
        
        paused = !paused;                               // Unpause the game
        Time.timeScale = 1;                             // activate the time
        inGaMenu.SetActive(false);                      // disable the canvas pause

    }   
    public void estadistiques_BTN() {

        estadistiques.SetActive(true);
        onStatistics = true;
    }
    public void back_BTN() {

        estadistiques.SetActive(false);
        onStatistics = false;
    }

    
    // SOME CALC FUNCTIONS
    float maxScoreCalc() {                              //regulates the difficulti of the next lvl 

        if (level <= 3) {
            return LastScore + (LastScore * 0.2f);      // the max score increases by 0.2 if lvl 3 is not reached
        }
        else
        {

            return LastScore + (LastScore * 0.05f);     // if lvl 3 its reached then the max score increases by 0.05
        }
       
    }
    void setTimeToZero()                                // If the time wents negative, its set to 0 
    {
        if (lifeTime <= 0)
        {
            lifeTime = 0;
        }
    }

    // UI
    void updateUI() {                                  // Updates the lvl number of the screen
        lvlText.text = "Lvl: " + level;                 // changes the text
    }

    // GENERATES A RANDOM LETTER:
    void generateRandomA() {                            // Generates the random letter

        if (!generated)                                 // if its not already generated...
        {
            for (int i = 0; i < 8; i++)                 // Disables all the letters generated before generating a new one
            {

                Tecles[i].SetActive(false);             // Disables all the components of the array where are all the letters.
            }

            a = Random.Range(0, nTecles);               // generates a number fom 0 to "numbes of letters (value depends on the lvl)"
            posX = Random.Range(-8, 8);                 // generates a random number for the transform X (into the main camera)
            posY = Random.Range(0, -7.3f);              // generates a random number for the transform Y (into the main camera)
            generated = true;                           
           
            Tecles[a].SetActive(true);                  // Activates the letter generated pseudo-randomly
            Tecles[a].transform.position = new Vector2(posX, posY); // we position the generated letter on the pseudo-random generated positions
        }
    }

    // KEYBOARD EVENTS
    void set_a_getKey() { // KEY EVENTS ==================================================================

        generateRandomA();  // we generate random x & y and letter "a" 

        if (Input.GetKeyDown(KeyCode.Escape)) {

            paused = !paused;

        }
        else if (generated && Input.GetKeyDown(KeyCode.Q) && a == 0)
        {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.W) && a == 1)
        {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.E) && a == 2)
        {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.R) && a == 3)
        {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.F) && a == 5) {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.GetKeyDown(KeyCode.D) && a == 4)
        {
            score++;
            encerts++;
            lifeTime++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(20, 186, 83, 255);
        }
        else if (generated && Input.anyKeyDown)
        {
            vides--;
            lifeTime--;
            errors++;
            generated = false;
            sliderColor.GetComponent<Image>().color = new Color32(191, 0, 0, 255);

        }
    }
}
