using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Makes sure that a button is selected on the main menu when the game starts.
/// </summary>
public class ChangeSelectionOnInput : MonoBehaviour {

    // Holds the event system for the menus
    [SerializeField]
    EventSystem eventSystem;

    // Object to select if there is not one currently
    [SerializeField]
    GameObject selectedObject;

    private bool buttonSelected;
	
	// Update is called once per frame
	void Update () {
        //If any vertical input is detected and no button has previously been selected, select selectedObject and set buttonSelected to true.
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    //When the main menu is disabled, no button is selected.
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
