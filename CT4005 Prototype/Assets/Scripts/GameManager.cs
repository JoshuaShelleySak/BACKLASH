using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the number of players in the game, instantiates them, keeps track of kills/wins as well as the final win condition.
/// </summary>
public class GameManager : MonoBehaviour {

    // The number of controllers that have joined.
    private int[] activeControllers = new int[4];

    // Prefabs for each of the players.
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] GameObject P3;
    [SerializeField] GameObject P4;

    // Current number of kills for each player
    private int P1Kills, P2Kills, P3Kills, P4Kills;

    // Current number of wins for each player
    private int P1Wins, P2Wins, P3Wins, P4Wins;

    // Keeps track of whether the players have been instantiated in the current scene
    private bool playersActivated;

    // Update is called once per frame
    void Update() {

        // If the current scene is the desert and the players haven't already been instantiated, instantiate them in the relevant positions for this map.
        if (SceneManager.GetActiveScene().buildIndex == 1 && playersActivated != true)
        {
            // Loops through the active controllers, checking if they have joined (by making sure the number is not 0), if they have, that player is instantiated.
            for (int i = 0; i < activeControllers.Length; i++)
            {
                if (activeControllers[i] != 0)
                {
                    switch (i)
                    {
                        case 0:
                            Vector3 p1Pos = new Vector3(0, 1, -10);
                            Instantiate(P1, p1Pos, Quaternion.identity);
                            break;
                        case 1:
                            Vector3 p2Pos = new Vector3(9, 1, 0);
                            Instantiate(P2, p2Pos, Quaternion.identity);
                            break;
                        case 2:
                            Vector3 p3Pos = new Vector3(-10, 1, 0);
                            Instantiate(P3, p3Pos, Quaternion.identity);
                            break;
                        case 3:
                            Vector3 p4Pos = new Vector3(0, 1, 9);
                            Instantiate(P4, p4Pos, Quaternion.identity);
                            break;
                    }
                }
            }
            // Makes sure this only happens once.
            playersActivated = true;
        }
        // Same as previously but checks if the scene is the saloon instead of the desert.
        else if (SceneManager.GetActiveScene().buildIndex == 3 && playersActivated != true)
        {
            Debug.Log("num of rounds" + PlayerPrefs.GetInt("numOfRounds"));
            for (int i = 0; i < activeControllers.Length; i++)
            {
                if (activeControllers[i] != 0)
                {
                    switch (i)
                    {
                        case 0:
                            Vector3 p1Pos = new Vector3(-10, 1, -10);
                            Instantiate(P1, p1Pos, Quaternion.identity);
                            break;
                        case 1:
                            Vector3 p2Pos = new Vector3(7, 1, -10);
                            Instantiate(P2, p2Pos, Quaternion.identity);
                            break;
                        case 2:
                            Vector3 p3Pos = new Vector3(7, 1, 7.5f);
                            Instantiate(P3, p3Pos, Quaternion.identity);
                            break;
                        case 3:
                            Vector3 p4Pos = new Vector3(-10, 1, 7.5f);
                            Instantiate(P4, p4Pos, Quaternion.identity);
                            break;
                    }
                }
            }
            playersActivated = true;
        }
    }

    // Used on the player joining screen.
    public void AddActiveController(int controllerPrefix)
    {
        // Adds the controller prefix to the corresponding place in the activeControllers array. Minusing the 1 makes so no more space is used than needed.
        activeControllers[controllerPrefix - 1] = controllerPrefix;
    }

    // Removes the controller at the relevant position by setting it to 0.
    public void RemoveActiveController(int controllerPrefix)
    {
        activeControllers[controllerPrefix - 1] = 0;
    }

    // Loops through array, setting it all to 0. Used on finishing of levels to make sure the right amount of players join each time.
    public void RemoveAllControllers()
    {
        for (int i = 0; i < activeControllers.Length; i++)
        {
            activeControllers[i] = 0;
        }
    }

    // Makes sure that only one GameManager is in the scene at one time to prevent duplicates causing errors.
    private void Awake()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameController");

        if (gameManagers.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Used PlayerHealthManager to up the number of kills for the player who last hit the dying player.
    public void IncreaseKills(int killerOfPlayer)
    {
        switch (killerOfPlayer)
        {
            case 1:
                P1Kills++;
                break;
            case 2:
                P2Kills++;
                break;
            case 3:
                P3Kills++;
                break;
            case 4:
                P4Kills++;
                break;
        }
    }

    // Increases the number of wins of the last player standing
    private void IncreaseWins(int controllerPrefixOfWinner)
    {
        if (controllerPrefixOfWinner == 1)
        {
            P1Wins++;
        }
        else if (controllerPrefixOfWinner == 2)
        {
            P2Wins++;
        }
        else if (controllerPrefixOfWinner == 3)
        {
            P3Wins++;
        }
        else if (controllerPrefixOfWinner == 4)
        {
            P4Wins++;
        }
    }

    // Used by the ResetGame class to either finish the game or reload the current level until all rounds have finished.
    public void ResetGame(int lastPlayerStanding)
    {
        //Lowers the number of rounds left
        PlayerPrefs.SetInt("numOfRounds", (PlayerPrefs.GetInt("numOfRounds") - 1));
        IncreaseWins(lastPlayerStanding);
        UpdateTotals();
        DeleteLeftoverPlayer();
        //Allows players to be instantiated again if the scene is going to be reloaded. 
        playersActivated = false;
        if (PlayerPrefs.GetInt("numOfRounds") == 0)
        {
            //Loads the leaderboard if all rounds have been completed.
            SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
            ResetTotals();
            return;
        }
        switch (PlayerPrefs.GetInt("mapSelected"))
        {
            case 0:
                SceneManager.LoadScene("Scene 1", LoadSceneMode.Single);
                break;
            case 1:
                SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
                break;
        }
    }

    // Updates the total kills and wins for each player. Used at the end of every round.
    private void UpdateTotals()
    {
        PlayerPrefs.SetInt("P1Kills", P1Kills);
        PlayerPrefs.SetInt("P2Kills", P2Kills);
        PlayerPrefs.SetInt("P3Kills", P3Kills);
        PlayerPrefs.SetInt("P4Kills", P4Kills);

        PlayerPrefs.SetInt("P1Wins", P1Wins);
        PlayerPrefs.SetInt("P2Wins", P2Wins);
        PlayerPrefs.SetInt("P3Wins", P3Wins);
        PlayerPrefs.SetInt("P4Wins", P4Wins);
    }

    // Removes the final standing player from the scene once the round is finished. Prevents duplicates from staying when the new round starts.
    private void DeleteLeftoverPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Destroy(player);
        }
    }

    // Resets the kills and wins of all players within the game manager for the next game.
    private void ResetTotals()
    {
        P1Kills = 0;
        P2Kills = 0;
        P3Kills = 0;
        P4Kills = 0;

        P1Wins = 0;
        P2Wins = 0;
        P3Wins = 0;
        P4Wins = 0;
    }
}
