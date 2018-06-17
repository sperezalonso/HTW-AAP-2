using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{

    float playerScore = 0;
    bool gameOver;

    private void Start()
    {
        gameOver = false;
    }

    void Update()
    {
        if (!gameOver) playerScore += Time.deltaTime;
    }

    // called whenever collectibles are picked up
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Score: " + (int)(playerScore * 100));

        if (gameOver)
        {
            GUI.Label(new Rect(Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
            if (GUI.Button(new Rect(Screen.width / 2 - 30, 350, 60, 30), "Retry?"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
