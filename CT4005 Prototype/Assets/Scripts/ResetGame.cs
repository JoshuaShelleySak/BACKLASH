using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Uses the gameManager to reset the game when there is only one player left alive. 
/// </summary>
public class ResetGame : MonoBehaviour
{
    private GameManager gm;

    //Finds the GameManager
    private void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }

    // This is ran every time a player dies (health goes below 0).
    public void CheckPlayercount()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length < 2)
        {
            //Final surviving player of the round
            GameObject finalPlayer = GameObject.FindGameObjectWithTag("Player");

            //Resets the game and passes the controller prefix of the final player to the GM. This allows for their wins to be increased.
            gm.ResetGame(finalPlayer.GetComponent<PlayerController>().GetPlayerControllerPrefix());
        }
    }
}