//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GunController : MonoBehaviour
//{

//    public bool bIsFiring;

//    [SerializeField] private BulletController bullet;
//    [SerializeField] private BulletController bcRifle, bcShotgun, bcMusket;
//    private float fBulletSpeed;
//    [SerializeField] private float fTimeBetweenShots;
//    [SerializeField] private Transform firePoint;
//    [SerializeField] private Transform firePoint2;
//    [SerializeField] private Transform firePoint3;
//    [SerializeField] private int controllerPrefix = 0;

//    [SerializeField] private GameObject gRevolver, gRifle, gShotgun, gMusket;

//    //private int playerShooting;
//    private float fShotCounter;


//    /// <summary>
//    /// 0 = Revolver
//    /// 1 = Rifle
//    /// 2 = Shotgun
//    /// 3 = Musket (Sniper)
//    /// 4 = Pirate Pistol
//    /// 5 = Cannon
//    /// </summary>
//    enum Guns
//    {
//        REVOLVER,
//        RIFLE,
//        SHOTGUN,
//        MUSKET,
//        PIRATE_PISTOL,
//        CANNON
//    };
//    private Guns iCurrentGun = 0;
//    private Guns[] iPlayerCurrentGuns = new Guns[6];

//    private bool gunToSwapToFound = false;

//    private void OnEnable()
//    {
//        //Adding revolver
//        AddGun(0);
//        SwapGun();
//    }

//    void SwapGun()
//    {
//        Debug.Log("current gun for " + controllerPrefix + " is:" + iCurrentGun);

//        switch (iCurrentGun)
//        {
//            case Guns.REVOLVER:
//                gRevolver.SetActive(true);
//                fBulletSpeed = 5;
//                fTimeBetweenShots = 0.75f;
//                bullet.iDamageToGive = 10;
//                bullet.fBulletSize = 0.3f;
//                bullet.iBulletBounceNum = 3;
//                bullet.bIsShotgun = false;
//                //Recoil
//                break;
//            case Guns.RIFLE:
//                fBulletSpeed = 8;
//                fTimeBetweenShots = 0.25f;
//                bullet.iDamageToGive = 10;
//                bullet.fBulletSize = 0.2f;
//                bullet.iBulletBounceNum = 4;
//                bullet.bIsShotgun = false;
//                //Recoil
//                break;
//            case Guns.SHOTGUN:
//                gShotgun.SetActive(true);
//                fBulletSpeed = 8;
//                fTimeBetweenShots = 1.5f;
//                bullet.iDamageToGive = 20;
//                bullet.fBulletSize = 0.3f;
//                bullet.iBulletBounceNum = 2;
//                bullet.bIsShotgun = true;
//                //Recoil
//                break;
//            case Guns.MUSKET:
//                gMusket.SetActive(true);
//                fBulletSpeed = 18;
//                fTimeBetweenShots = 3f;
//                bullet.iDamageToGive = 25;
//                bullet.fBulletSize = 0.3f;
//                bullet.iBulletBounceNum = 5;
//                bullet.bIsShotgun = false;
//                //Recoil
//                break;
//            case Guns.PIRATE_PISTOL:
//                fBulletSpeed = 6;
//                fTimeBetweenShots = 0.75f;
//                bullet.iDamageToGive = 15;
//                bullet.fBulletSize = 0.3f;
//                bullet.iBulletBounceNum = 3;
//                bullet.bIsShotgun = false;
//                //Recoil
//                break;
//            case Guns.CANNON:
//                fBulletSpeed = 4;
//                fTimeBetweenShots = 9999999999f;
//                bullet.iDamageToGive = 10;
//                bullet.fBulletSize = 0.6f;
//                bullet.iBulletBounceNum = 1;
//                bullet.bIsShotgun = false;
//                //Recoil
//                break;
//        }
//    }


//    void Update()
//    {

//        gunToSwapToFound = false;

//        //if (Input.GetButtonDown("P" + controllerPrefix + "SwapLeft"))
//        //{
//        //    SwapLeft();
//        //}

//        //if (Input.GetButtonDown("P" + controllerPrefix + "SwapRight"))
//        //{
//        //    SwapRight();
//        //}

//        //Right on d-pad
//        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadX") == 1 && iCurrentGun != Guns.SHOTGUN)
//        {
//            if (CheckForGun(Guns.SHOTGUN))
//            {
//                iCurrentGun = Guns.SHOTGUN;
//                SwapGun();
//            }
//        }

//        //Left on d-pad
//        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadX") == -1 && iCurrentGun != Guns.RIFLE)
//        {
//            if (CheckForGun(Guns.RIFLE))
//            {
//                iCurrentGun = Guns.RIFLE;
//                SwapGun();
//            }
//        }

//        //Up on d-pad
//        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadY") == 1 && iCurrentGun != Guns.REVOLVER)
//        {
//            if (CheckForGun(Guns.REVOLVER))
//            {
//                iCurrentGun = Guns.REVOLVER;
//                SwapGun();
//            }
//        }

//        //Down on d-pad
//        if (Input.GetAxisRaw("P" + controllerPrefix + "DpadY") == -1 && iCurrentGun != Guns.MUSKET)
//        {
//            if (CheckForGun(Guns.MUSKET))
//            {
//                iCurrentGun = Guns.MUSKET;
//                SwapGun();
//            }
//        }

//        fShotCounter -= Time.deltaTime;

//        if (bIsFiring)
//        {
//            if (fShotCounter <= 0)
//            {
//                fShotCounter = fTimeBetweenShots;
//                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
//                if (bullet.bIsShotgun == true)
//                {
//                    BulletController newBullet2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation) as BulletController;
//                    BulletController newBullet3 = Instantiate(bullet, firePoint3.position, firePoint3.rotation) as BulletController;

//                    switch (controllerPrefix)
//                    {
//                        case 1:
//                            newBullet2.GetComponent<Renderer>().material.color = Color.green;
//                            newBullet3.GetComponent<Renderer>().material.color = Color.green;
//                            break;
//                        case 2:
//                            newBullet2.GetComponent<Renderer>().material.color = Color.red;
//                            newBullet3.GetComponent<Renderer>().material.color = Color.red;
//                            break;
//                        case 3:
//                            newBullet2.GetComponent<Renderer>().material.color = Color.blue;
//                            newBullet3.GetComponent<Renderer>().material.color = Color.blue;
//                            break;
//                        case 4:
//                            newBullet2.GetComponent<Renderer>().material.color = Color.yellow;
//                            newBullet3.GetComponent<Renderer>().material.color = Color.yellow;
//                            break;
//                        case 5:
//                            newBullet2.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
//                            newBullet3.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
//                            break;
//                    }

//                    newBullet2.fBulletSpeed = fBulletSpeed;
//                    newBullet3.fBulletSpeed = fBulletSpeed;
//                }
//                switch (controllerPrefix)
//                {
//                    case 1:
//                        newBullet.GetComponent<Renderer>().material.color = Color.green;
//                        break;
//                    case 2:
//                        newBullet.GetComponent<Renderer>().material.color = Color.red;
//                        break;
//                    case 3:
//                        newBullet.GetComponent<Renderer>().material.color = Color.blue;
//                        break;
//                    case 4:
//                        newBullet.GetComponent<Renderer>().material.color = Color.yellow;
//                        break;
//                    case 5:
//                        newBullet.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f);
//                        break;
//                }
//                newBullet.fBulletSpeed = fBulletSpeed;
//            }
//        }
//    }

//    bool CheckForGun(Guns gunToSearchFor)
//    {
//        if ((int)gunToSearchFor > iPlayerCurrentGuns.Length || gunToSearchFor < 0)
//        {
//            Debug.Log("false");
//            return false;
//        }
//        if (iPlayerCurrentGuns[(int)gunToSearchFor] == gunToSearchFor)
//        {
//            Debug.Log("true");
//            return true;
//        }
//        return false;
//    }

//    public void AddGun(int gunToBeAdded)
//    {
//        //Adds the gun that has been picked up to the corresponding place in the array
//        //Makes checks easier since there will be no need to cycle the entire array to look for the gun
//        iPlayerCurrentGuns[gunToBeAdded] = (Guns)gunToBeAdded;
//        Debug.Log((Guns)gunToBeAdded);
//    }

//    void SwapLeft()
//    {
//        Debug.Log("swap left started");

//        for (int i = (int)iCurrentGun - 1; gunToSwapToFound == true; i--)
//        {
//            Debug.Log("left" + i);
//            if (CheckForGun((Guns)i))
//            {
//                iCurrentGun = (Guns)i;
//                gunToSwapToFound = true;
//                break;
//            }
//        }

//        //for (int i = (iPlayerCurrentGuns.Length - (int)iCurrentGun); i == 0; i--)
//        //{
//        //    Debug.Log("left" + i);
//        //    if (CheckForGun((Guns)i))
//        //    {
//        //        iCurrentGun = (Guns)i;
//        //        gunToSwapToFound = true;
//        //        break;
//        //    }
//        //}

//        if (gunToSwapToFound == false)
//        {
//            Debug.Log("looping back around");
//            for (int i = (int)iCurrentGun + 1; i < iPlayerCurrentGuns.Length; i++)
//            {
//                if (CheckForGun((Guns)i))
//                {
//                    iCurrentGun = (Guns)i;
//                    gunToSwapToFound = true;
//                    break;
//                }
//            }
//        }

//        SwapGun();
//    }

//    void SwapRight()
//    {
//        for (int i = (int)iCurrentGun + 1; i < iPlayerCurrentGuns.Length; i++)
//        {
//            Debug.Log("right" + 1);
//            if (CheckForGun((Guns)i))
//            {
//                iCurrentGun = (Guns)i;
//                gunToSwapToFound = true;
//                break;
//            }
//        }

//        if (gunToSwapToFound == false)
//        {
//            for (int i = 0; i < (int)iCurrentGun; i++)
//            {
//                if (CheckForGun((Guns)i))
//                {
//                    iCurrentGun = (Guns)i;
//                    gunToSwapToFound = true;
//                    break;
//                }
//            }
//        }

//        SwapGun();
//    }
//}
