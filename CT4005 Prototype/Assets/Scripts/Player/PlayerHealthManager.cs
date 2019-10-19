using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This handles the health of each player and invincibility frames between being damaged.
/// It updates the UI of each player to the current int value.
/// When a player dies, it checks the last player they were hit by in order to keep track of player kills.
/// When someone dies, it calls a playercount to see how many players are left.
/// </summary>

public class PlayerHealthManager : MonoBehaviour {

    
    private float startingHealth = 100;
    private float currentHealth;

    [SerializeField] private Image healthBar;

    GameManager gameManager;

    public bool canBeDamaged = true;

    private float flashLength = 0.4f;
    private float flashCounter;

    private Renderer rend;
    private Color storedColour;

    private int lastHitBy;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = startingHealth;
        rend = GetComponent<Renderer>();
        storedColour = rend.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0)
        {
            if (lastHitBy != this.GetComponent<PlayerController>().GetPlayerControllerPrefix())
            {
                gameManager.IncreaseKills(lastHitBy);
            }
            gameObject.SetActive(false);
            gameManager.GetComponent<ResetGame>().CheckPlayercount();
        }

        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                canBeDamaged = true;
                rend.material.SetColor("_Color", storedColour);
            }
        }
	}

    public void HurtPlayer(int damageAmount, int playerWhoHit)
    {
        canBeDamaged = false;
        currentHealth -= damageAmount;
        healthBar.fillAmount = (currentHealth / startingHealth);
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);
        lastHitBy = playerWhoHit;
    }

}
