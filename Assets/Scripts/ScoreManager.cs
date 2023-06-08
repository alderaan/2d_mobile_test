using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI myText; // Reference to your TextMeshPro object
    public GameObject player; // Reference to your player GameObject

    // Update is called once per frame
    void Update()
    {
        // Get the player's y position, round it to the nearest integer
        int playerYPosition = Mathf.RoundToInt(player.transform.position.y) * 100;

        // Update the text of your TextMeshPro object
        myText.text = "Score: " + playerYPosition.ToString();
    }
}
