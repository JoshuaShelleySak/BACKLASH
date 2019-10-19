using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows players to join based on controller prefix. Attached to each of the player's join texts.
/// </summary>
public class PlayerJoin : MonoBehaviour {

    [SerializeField] private int controllerPrefix = 0;
    [SerializeField] private GameManager gm;

    private bool playerJoined = false;

    // Update is called once per frame
    void Update()
    {
        // If the player has not already joined and the 'A' button has been pressed. The player is added to active controllers and the text is changed to inform the player.
        if (Input.GetButtonDown("P" + controllerPrefix +"Submit") && !playerJoined)
        {
            this.gameObject.GetComponent<Text>().text = "Player" + controllerPrefix + " joined.";
            gm.AddActiveController(controllerPrefix);
            SetJoined(true);
        }
        // Previously used for testing. Outdated.
        if (Input.GetKeyDown("e"))
        {
            this.gameObject.GetComponent<Text>().text = "Player 1 joined.";
            gm.AddActiveController(controllerPrefix);
            SetJoined(true);
        }
    }
    
    public bool GetJoined()
    {
        return playerJoined;
    }

    //Changes the join state to the boolean passed into it. If it is set to false, it sets the text back to it's default.
    public void SetJoined(bool joinState)
    {
        if (joinState == false)
        {
            this.gameObject.GetComponent<Text>().text = "Press (A) to join.";
        }
        playerJoined = joinState;
    }
}
