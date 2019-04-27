using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    private enum GameState
    {
        game,pause,
    }
    private GameState state;
    private Vector3 startPosition;
    private float timer;
    private float highScore;
    private string timerString;
    private Text TimerUI;
    private Text GameOverUI;
    
    public float spawnTime;
    
    void Start()
    {
        // SINGLETON
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        //INIT
        startPosition = GameObject.FindWithTag("Player").transform.position;
        //can call ReloadGame() here
        timer = 0f;
        TimerUI = GameObject.Find("TimerUI").GetComponent<Text>();
        GameOverUI = GameObject.Find("GameOverUI").GetComponent<Text>();
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnTime);
    }


    void Update()
    {
        switch (state)
        {
            case GameState.game:
                RunTimer();
                break;
            case GameState.pause:
                Time.timeScale = 0f;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //TODO:Write a high score saving function
                    ReloadGame();
                }
                break;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy"));
        enemy.transform.rotation = Random.rotation;
    }

    public void GameOver()
    {
      highScore = timer;
      GameOverUI.text = "Game Over!!!";
      state = GameState.pause;
    }
    
    void ReloadGame()
    {
        Time.timeScale = 1f;
        timer = 0f;
        ClearScene();
        GameOverUI.text = "";
        GameObject.Find("Player").transform.position = startPosition;
        Instantiate(Resources.Load("Prefabs/Enemy"));
        state = GameState.game;
    }

    void ClearScene()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(obj);
        }
    }

    void RunTimer()
    {
        timer += Time.deltaTime;
        timerString = timer.ToString("F0");
        TimerUI.text = "Time Elapsed: " + timerString;
    }

}
