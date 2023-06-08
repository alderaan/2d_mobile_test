using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float gameStartTime;
    public float timeSinceGameStart;
    public ScoreManager scoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        // Store the time when the game starts
        gameStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceGameStart = Time.time - gameStartTime;
    }

    public void ResetGame()
    {
        // Save Highscore
        scoreManager.SaveHighScore();
        
        // Set new gameStartTime
        gameStartTime = Time.time;
        
        // Reset all game variables to their initial values...

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
