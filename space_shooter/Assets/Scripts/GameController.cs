using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject Background;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float speed = 100f;

    public Text ScoreText;
    public Text winText;
    public Text restartText;
    public Text gameOverText;

    public AudioClip win;
    public AudioClip lose;


    private AudioSource audioSource;
    public ParticleSystem particle1;
    public ParticleSystem particle2;

    private bool gameOver;
    private bool sound;
    private bool restart;
    public bool hard;
    public bool scroll;
    private int score;


    void Start ()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        audioSource = GetComponent<AudioSource>();
    }


   void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 

        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.N)){

                SceneManager.LoadScene("Main");


            }

           
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            if (hard == true)
            {
                hard = false;
            }

            else if (hard == false)
            {
                hard = true;
            }
        }  

        if (hard == true)
        {
            hazards[0].GetComponent<Mover>().speed = -10f;
            hazards[1].GetComponent<Mover>().speed = -10f;
            hazards[2].GetComponent<Mover>().speed = -10f;
            hazards[3].GetComponent<Mover>().speed = -10f;
            hazards[4].GetComponent<Mover>().speed = -7f;

        }

        if (hard == false)
        {
            hazards[0].GetComponent<Mover>().speed = -5f;
            hazards[1].GetComponent<Mover>().speed = -5f;
            hazards[2].GetComponent<Mover>().speed = -5f;
            hazards[3].GetComponent<Mover>().speed = -5f;
            hazards[4].GetComponent<Mover>().speed = -5f;

        }

        if (scroll == true)
        {
            Background.GetComponent<BGScroller>().scrollSpeed = 5f;
        }

        if (sound == true)
        {
            audioSource.Pause();
            audioSource.clip = win;

            audioSource.Play();
        }


        
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'N' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            ParticleSystem.MainModule psMain = particle1.main;
            ParticleSystem.MainModule ps2Main = particle2.main;
            psMain.simulationSpeed = speed;
            ps2Main.simulationSpeed = speed;
            winText.text = "You Win! Game Created by: Juan Quevedo"  + "  Press 'H' After Restart to Test HARD MODE!";
            restartText.text = "Press 'N' for Restart";
            gameOver = true;
            restart = true;
      

            sound = true;
            scroll = true;
        }

    }



    public void GameOver()
    {
        gameOverText.text = "Game Over!";

        gameOver = true;

        audioSource.Pause();
        audioSource.clip = lose;
        gameObject.GetComponent<AudioSource>().PlayOneShot(lose, 0.4f);
    }

}

