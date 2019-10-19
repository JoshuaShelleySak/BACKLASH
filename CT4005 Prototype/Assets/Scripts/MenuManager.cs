using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used on the join screen to check whether or not any players have joined. Enables the start button if any have.
/// </summary>
public class MenuManager : MonoBehaviour {

    //The panels that display whether or not a player has joined.
    [SerializeField] PlayerJoin p1Join;
    [SerializeField] PlayerJoin p2Join;
    [SerializeField] PlayerJoin p3Join;
    [SerializeField] PlayerJoin p4Join;

    [SerializeField] Image startButtonPanel;
	
	// Update is called once per frame
	void Update () {
        if (p1Join.GetJoined() || p2Join.GetJoined() || p3Join.GetJoined() || p4Join.GetJoined())
        {
            startButtonPanel.gameObject.SetActive(true);
        }
	}

    //Used by the back button on the join screen to reset the number of players joined and to de-activate the start button panel.
    public void ResetPlayerJoins()
    {
        startButtonPanel.gameObject.SetActive(false);
        p1Join.SetJoined(false);
        p2Join.SetJoined(false);
        p3Join.SetJoined(false);
        p4Join.SetJoined(false);
    }
}
