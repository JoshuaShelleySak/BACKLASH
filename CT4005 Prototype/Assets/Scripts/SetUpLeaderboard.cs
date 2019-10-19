using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds all of the relevant information to the leaderboard on the endscreen.
/// </summary>
public class SetUpLeaderboard : MonoBehaviour {

    //Each of the different score panels for the players.
    [SerializeField] private RectTransform p1Panel, p2Panel, p3Panel, p4Panel;

    private Text p1Kills, p2Kills, p3Kills, p4Kills;
    private Text p1Wins, p2Wins, p3Wins, p4Wins;

	// Use this for initialization
	void Start () {
        FindAllKillAndWinText();
        UpdateKillAndWinText();
    }

    //Finds the text components that hold the total kills and wins for each player by looking for the children of the relative player's panel.
    void FindAllKillAndWinText()
    {
        p1Kills = p1Panel.transform.GetChild(1).GetComponent<Text>();
        p1Wins = p1Panel.transform.GetChild(2).GetComponent<Text>();

        p2Kills = p2Panel.transform.GetChild(1).GetComponent<Text>();
        p2Wins = p2Panel.transform.GetChild(2).GetComponent<Text>();

        p3Kills = p3Panel.transform.GetChild(1).GetComponent<Text>();
        p3Wins = p3Panel.transform.GetChild(2).GetComponent<Text>();

        p4Kills = p4Panel.transform.GetChild(1).GetComponent<Text>();
        p4Wins = p4Panel.transform.GetChild(2).GetComponent<Text>();
    }

    //Changes the kills and wins text to the PlayerPrefs previously set by the GameManager while the rounds were running.
    void UpdateKillAndWinText()
    {
        p1Kills.text = "Kills: " + PlayerPrefs.GetInt("P1Kills").ToString();
        p2Kills.text = "Kills: " + PlayerPrefs.GetInt("P2Kills").ToString();
        p3Kills.text = "Kills: " + PlayerPrefs.GetInt("P3Kills").ToString();
        p4Kills.text = "Kills: " + PlayerPrefs.GetInt("P4Kills").ToString();

        p1Wins.text = "Wins: " + PlayerPrefs.GetInt("P1Wins").ToString();
        p2Wins.text = "Wins: " + PlayerPrefs.GetInt("P2Wins").ToString();
        p3Wins.text = "Wins: " + PlayerPrefs.GetInt("P3Wins").ToString();
        p4Wins.text = "Wins: " + PlayerPrefs.GetInt("P4Wins").ToString();
    }
}
