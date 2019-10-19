using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Allows for navigation in the character select menu, based on whether players have already joined.
/// </summary>
public class CharacterSelectMenuNavigation : MonoBehaviour {

    [SerializeField]
    Button backButton;

    [SerializeField]
    Button startButton;

    private bool buttonSelected;
	
	// Update is called once per frame
	void Update () {
        // If the analog stick is pushed down and no button is selected, a button is selected based on whether or not players have already joined. (As players joining causes the startButton to become active)
        if ((Input.GetAxis("P1Vertical") == -1))
        {
            if (!buttonSelected)
            {
                buttonSelected = true;
                if (startButton.IsActive())
                {
                    startButton.Select();
                }
                else
                {
                    backButton.Select();
                }
            }
        }
        // This makes it so that the other players can still join without being stuck on the back button.
        // Deselects the back button if the analog stick is pushed up.
        else if ((Input.GetAxis("P1Vertical") == 1) && !startButton.IsActive())
        {
            if (buttonSelected)
            {
                buttonSelected = false;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        
	}

    // Used by the start/play button, makes it so if the players go back onto this screen, they don't get stuck.
    public void DeselectButtons()
    {
        buttonSelected = false;
    }
}
