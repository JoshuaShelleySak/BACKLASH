using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls gun switching, player inventory management and firing the gun.
/// </summary>
public class GunController : MonoBehaviour {

    //Whether the gun is firing
    public bool bIsFiring;

    //The bullet controller used by the gun
    private BulletController bullet;
    //The bullet controller for each of the weapons - allows for different fire speeds and damages.
    [SerializeField] private BulletController bcRevolver, bcRifle, bcShotgun, bcMusket;
    //Speed of the bullet
    private float fBulletSpeed;
    //Reload time
    [SerializeField] private float fTimeBetweenShots;
    //Where the bulelts are fired from. Firepoint2 and FirePoint3 
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private int controllerPrefix = 0;

    //Objects of each of the weapons, are activated when it is swapped to
    [SerializeField] private GameObject gRevolver, gRifle, gShotgun, gMusket;
    //Sound effects for each of the weapons
    [SerializeField] private AudioSource aRevolver, aRifle, aShotgun, aMusket;

    //private int playerShooting;
    private float fShotCounter;
    
    /// <summary>
    /// 0 = Revolver
    /// 1 = Rifle
    /// 2 = Shotgun
    /// 3 = Musket (Sniper)
    /// </summary>
    enum Guns
    {
        REVOLVER,
        RIFLE,
        SHOTGUN,
        MUSKET,
    };
    //Currently selected weapons
    private Guns iCurrentGun = 0;
    //Weapons currently in the player's inventory
    private Guns[] iPlayerCurrentGuns = new Guns[4];

    //Helps track whether or not the gun has been found
    private bool gunToSwapToFound = false;

    //Adds the revolver and swaps to it when the gun/player is instantiated
    private void OnEnable()
    {
        //Adding revolver
        AddGun(0);
        SwapGun();
    }

    //Chanes the gun/bullet variables and models to the currently selected gun.
    void SwapGun()
    {
        Debug.Log("current gun for " + controllerPrefix + " is:" + iCurrentGun);

        //Checks which gun is currently selected and changes the corresponding variables to fix it as well as activating the model for the gun
        switch (iCurrentGun)
        {
            case Guns.REVOLVER:
                bullet = bcRevolver;
                gRevolver.SetActive(true);
                fBulletSpeed = 5;
                fTimeBetweenShots = 0.75f;
                bullet.iDamageToGive = 10;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 3;
                bullet.bIsShotgun = false;
                //Recoil
                break;
            case Guns.RIFLE:
                bullet = bcRifle;
                gRifle.SetActive(true);
                fBulletSpeed = 8;
                fTimeBetweenShots = 0.25f;
                bullet.iDamageToGive = 10;
                bullet.fBulletSize = 0.2f;
                bullet.iBulletBounceNum = 4;
                bullet.bIsShotgun = false;
                //Recoil
                break;
            case Guns.SHOTGUN:
                bullet = bcShotgun;
                gShotgun.SetActive(true);
                fBulletSpeed = 8;
                fTimeBetweenShots = 1.5f;
                bullet.iDamageToGive = 20;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 2;
                bullet.bIsShotgun = true;
                //Recoil
                break;
            case Guns.MUSKET:
                bullet = bcMusket;
                gMusket.SetActive(true);
                fBulletSpeed = 18;
                fTimeBetweenShots = 3f;
                bullet.iDamageToGive = 25;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 5;
                bullet.bIsShotgun = false;
                //Recoil
                break;
        }
    }


    void Update () {
        //Sets it to false every frame to make sure no gun is accidentally found
        gunToSwapToFound = false;

        //Right on d-pad and if the currently selected gun isn't already the one trying to be swapped to
        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadX") == 1 && iCurrentGun != Guns.SHOTGUN)            
        {
            //Checks to see if the player has picked up the shotgun
            if (CheckForGun(Guns.SHOTGUN))
            {
                //Hides all of the gun models
                HideAllGuns();
                //Changes the currently selected gun to the correct weapon
                iCurrentGun = Guns.SHOTGUN;
                //Runs the swapGun function, this also enables the correct model
                SwapGun();
            }
        }

        //Left on d-pad and if the currently selected gun isn't already the one trying to be swapped to
        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadX") == -1 && iCurrentGun != Guns.RIFLE)
        {
            //Checks to see if the player has picked up the rifle
            if (CheckForGun(Guns.RIFLE))
            {
                //Hides all of the gun models
                HideAllGuns();
                //Changes the currently selected gun to the correct weapon
                iCurrentGun = Guns.RIFLE;
                //Runs the swapGun function, this also enables the correct model
                SwapGun();
            }
        }

        //Up on d-pad and if the currently selected gun isn't already the one trying to be swapped to
        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadY") == 1 && iCurrentGun != Guns.REVOLVER)
        {
            //Checks to see if the player has picked up the revolver
            if (CheckForGun(Guns.REVOLVER))
            {
                //Hides all of the gun models
                HideAllGuns();
                //Changes the currently selected gun to the correct weapon
                iCurrentGun = Guns.REVOLVER;
                //Runs the swapGun function, this also enables the correct model
                SwapGun();
            }
        }

        //Down on d-pad and if the currently selected gun isn't already the one trying to be swapped to
        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadY") == -1 && iCurrentGun != Guns.MUSKET)
        {
            //Checks to see if the player has picked up the musket
            if (CheckForGun(Guns.MUSKET))
            {
                //Hides all of the gun models
                HideAllGuns();
                //Changes the currently selected gun to the correct weapon
                iCurrentGun = Guns.MUSKET;
                //Runs the swapGun function, this also enables the correct model
                SwapGun();
            }
        }

        //Lowers the time until the next shot is allowed by deltaTime
        fShotCounter -= Time.deltaTime;
        
        //If the gun is firing
        if (bIsFiring)
        {
            //And there is no reload time left
            if(fShotCounter <= 0)
            {
                //Play the shot sound corresponding to the current selected gun
                switch (iCurrentGun)
                {
                    case Guns.REVOLVER:
                        aRevolver.Play();
                        break;
                    case Guns.RIFLE:
                        aRifle.Play();
                        break;
                    case Guns.SHOTGUN:
                        aShotgun.Play();
                        break;
                    case Guns.MUSKET:
                        aMusket.Play();
                        break;
                }

                //Reload time is reset
                fShotCounter = fTimeBetweenShots;
                //Bullet is instantiating at the firePoint
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                //Updates the playerWhoShot to be the gunController controllerPrefix
                newBullet.iPlayerWhoShot = controllerPrefix;
                //If the weapon is a shotgun, fire two more bullets
                if (bullet.bIsShotgun == true)
                {
                    BulletController newBullet2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation) as BulletController;
                    newBullet2.iPlayerWhoShot = controllerPrefix;

                    BulletController newBullet3 = Instantiate(bullet, firePoint3.position, firePoint3.rotation) as BulletController;
                    newBullet3.iPlayerWhoShot = controllerPrefix;

                    //Changes the bullet material colour based on the controller prefix
                    switch (controllerPrefix)
                    {
                        case 1:
                            newBullet2.GetComponent<Renderer>().material.color = Color.green;
                            newBullet3.GetComponent<Renderer>().material.color = Color.green;
                            break;
                        case 2:
                            newBullet2.GetComponent<Renderer>().material.color = Color.red;
                            newBullet3.GetComponent<Renderer>().material.color = Color.red;
                            break;
                        case 3:
                            newBullet2.GetComponent<Renderer>().material.color = Color.blue;
                            newBullet3.GetComponent<Renderer>().material.color = Color.blue;
                            break;
                        case 4:
                            newBullet2.GetComponent<Renderer>().material.color = Color.yellow;
                            newBullet3.GetComponent<Renderer>().material.color = Color.yellow;
                            break;
                        case 5:
                            newBullet2.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
                            newBullet3.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
                            break;
                    }

                    //Set the bullet speed of the two bullets
                    newBullet2.fBulletSpeed = fBulletSpeed;
                    newBullet3.fBulletSpeed = fBulletSpeed;
                }

                //Changes the bullet material colour based on the controller prefix
                switch (controllerPrefix)
                {
                    case 1:
                        newBullet.GetComponent<Renderer>().material.color = Color.green;
                        break;
                    case 2:
                        newBullet.GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case 3:
                        newBullet.GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case 4:
                        newBullet.GetComponent<Renderer>().material.color = Color.yellow;
                        break;
                    case 5:
                        newBullet.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
                        break;
                }
                //Sets the bullet speed
                newBullet.fBulletSpeed = fBulletSpeed;
            }
        }
    }

    //Returns a bool based on whether or not the gun was found in the inventory
    bool CheckForGun(Guns gunToSearchFor)
    {
        //If the gun searched for is out of range, return false
        if ((int)gunToSearchFor > iPlayerCurrentGuns.Length || gunToSearchFor < 0)
        {
            Debug.Log("false");
            return false;
        }
        //The gun is in the player's inventory at the correct place, return true
        if (iPlayerCurrentGuns[(int)gunToSearchFor] == gunToSearchFor)
        {
            Debug.Log("true");
            return true;
        }
        //If nothing happens, return false
        return false;
    }

    //Adds the gunToBeAdded to the currentGunsArray/player inventory
    public void AddGun(int gunToBeAdded)
    {
        //Adds the gun that has been picked up to the corresponding place in the array
        //Makes checks easier since there will be no need to cycle the entire array to look for the gun
        iPlayerCurrentGuns[gunToBeAdded] = (Guns)gunToBeAdded;
        Debug.Log((Guns)gunToBeAdded);
    }

    //Sets all of the gun models to be inactive. Used when guns are being swapped.
    private void HideAllGuns()
    {
        gRevolver.SetActive(false);
        gShotgun.SetActive(false);
        gMusket.SetActive(false);
        gRifle.SetActive(false);
    }
}
