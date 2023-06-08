using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI myText; // Reference to your TextMeshPro object
    public GameObject player; // Reference to your player GameObject
    int playerYPosition = 0;
    int playerScore = 0;

    // Update is called once per frame
    void Update()
    {
        // Get the player's y position, round it to the nearest integer
        playerYPosition = Mathf.RoundToInt(player.transform.position.y);

        if (playerYPosition * 100 > playerScore)
        {
            playerScore = playerYPosition * 100;
        }


        // Update the text of your TextMeshPro object
        myText.text = "Score: " + playerScore.ToString();
    }
}
