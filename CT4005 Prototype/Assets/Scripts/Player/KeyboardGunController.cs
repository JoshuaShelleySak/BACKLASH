using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Same as gunController but for keyboard and mouse. Outdated and now unused. Was previously used for testing.
/// </summary>
public class KeyboardGunController : MonoBehaviour
{

    public bool bIsFiring;

    [SerializeField] private BulletController bullet;
    private float fBulletSpeed;
    [SerializeField] private float fTimeBetweenShots;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform firePoint2;
    [SerializeField] private Transform firePoint3;
    [SerializeField] private int controllerPrefix = 0;

    [SerializeField] private GameObject gRevolver, gShotgun, gMusket, gRifle;
    [SerializeField] private AudioSource aRevolver, aRifle, aShotgun, aMusket;

    //private int playerShooting;
    private float fShotCounter;
    private bool bIsShotgun;


    /// <summary>
    /// 0 = Revolver
    /// 1 = Rifle
    /// 2 = Shotgun
    /// 3 = Musket (Sniper)
    /// 4 = Pirate Pistol
    /// 5 = Cannon
    /// </summary>
    /// 

    enum Guns
    {
        REVOLVER,
        RIFLE,
        SHOTGUN,
        MUSKET,
        PIRATE_PISTOL,
        CANNON
    };
    private Guns iCurrentGun = 0;
    private Guns[] iPlayerCurrentGuns = new Guns[6];

    private void OnEnable()
    {
        //Adding revolver
        AddGun(0);
        SwapGun();
    }

    void SwapGun()
    {
        Debug.Log("current gun for " + controllerPrefix + " is:" + iCurrentGun);
        switch (iCurrentGun)
        {
            case Guns.REVOLVER:
                gRevolver.SetActive(true);
                fBulletSpeed = 5;
                fTimeBetweenShots = 0.75f;
                bullet.iDamageToGive = 10;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 3;
                bIsShotgun = false;
                //Recoil
                break;
            case Guns.RIFLE:
                gRifle.SetActive(true);
                fBulletSpeed = 8;
                fTimeBetweenShots = 0.25f;
                bullet.iDamageToGive = 10;
                bullet.fBulletSize = 0.2f;
                bullet.iBulletBounceNum = 4;
                bIsShotgun = false;
                //Recoil
                break;
            case Guns.SHOTGUN:
                gShotgun.SetActive(true);
                fBulletSpeed = 8;
                fTimeBetweenShots = 1.5f;
                bullet.iDamageToGive = 20;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 2;
                bIsShotgun = true;
                //Recoil
                break;
            case Guns.MUSKET:
                gMusket.SetActive(true);
                fBulletSpeed = 18;
                fTimeBetweenShots = 3f;
                bullet.iDamageToGive = 25;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 5;
                bIsShotgun = false;
                //Recoil
                break;
            case Guns.PIRATE_PISTOL:
                fBulletSpeed = 6;
                fTimeBetweenShots = 0.75f;
                bullet.iDamageToGive = 15;
                bullet.fBulletSize = 0.3f;
                bullet.iBulletBounceNum = 3;
                bIsShotgun = false;
                //Recoil
                break;
            case Guns.CANNON:
                fBulletSpeed = 4;
                fTimeBetweenShots = 9999999999f;
                bullet.iDamageToGive = 10;
                bullet.fBulletSize = 0.6f;
                bullet.iBulletBounceNum = 1;
                bIsShotgun = false;
                //Recoil
                break;
        }
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1) && CheckForGun(Guns.REVOLVER))
        {
            HideAllGuns();
            iCurrentGun = Guns.REVOLVER;
            SwapGun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && CheckForGun(Guns.RIFLE))
        {
            HideAllGuns();
            iCurrentGun = Guns.RIFLE;
            SwapGun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && CheckForGun(Guns.SHOTGUN))
        {
            HideAllGuns();
            iCurrentGun = Guns.SHOTGUN;
            SwapGun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && CheckForGun(Guns.MUSKET))
        {
            HideAllGuns();
            iCurrentGun = Guns.MUSKET;
            SwapGun();
        }

        fShotCounter -= Time.deltaTime;

        if (bIsFiring)
        {

            if (fShotCounter <= 0)
            {
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

                fShotCounter = fTimeBetweenShots;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                if (bIsShotgun == true)
                {
                    BulletController newBullet2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation) as BulletController;
                    BulletController newBullet3 = Instantiate(bullet, firePoint3.position, firePoint3.rotation) as BulletController;

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

                    newBullet2.fBulletSpeed = fBulletSpeed;
                    newBullet3.fBulletSpeed = fBulletSpeed;
                }

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
                newBullet.fBulletSpeed = fBulletSpeed;
            }
        }
    }

    bool CheckForGun(Guns gunToSearchFor)
    {
        if (iPlayerCurrentGuns[(int)gunToSearchFor] == gunToSearchFor)
        {
            return true;
        }
        return false;
    }

    public void AddGun(int gunToBeAdded)
    {
        //Adds the gun that has been picked up to the corresponding place in the array
        //Makes checks easier since there will be no need to cycle the entire array to look for the gun
        iPlayerCurrentGuns[gunToBeAdded] = (Guns)gunToBeAdded;
        Debug.Log((Guns)gunToBeAdded);
    }

    private void HideAllGuns()
    {
        gRevolver.SetActive(false);
        gShotgun.SetActive(false);
        gMusket.SetActive(false);
        gRifle.SetActive(false);
    }
}
