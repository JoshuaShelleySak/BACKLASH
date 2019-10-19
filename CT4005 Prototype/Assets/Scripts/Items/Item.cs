using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the prefabs for each of the weapons so that they can be shown on the ground after spawning. Sets the IDs which are used to know which item is being picked up.
/// </summary>
public class Item : MonoBehaviour {

    /// <summary>
    /// 0 = Revolver
    /// 1 = Rifle
    /// 2 = Shotgun
    /// 3 = Musket (Sniper)
    /// </summary>
    [SerializeField]
    int iItemID = 0;

    //Prefabs for each of the potential item spawns.
    [SerializeField] private GameObject gRevolver, gRifle, gShotgun, gMusket;

    public int GetItemID()
    {
        return iItemID;
    }

    //Sets the itemID then sets the corresponding model to be active.
    public void SetItemID(int IDToBeSet)
    {
        iItemID = IDToBeSet;

        switch (iItemID)
        {
            case 0:
                gRevolver.SetActive(true);
                break;
            case 1:
                gRifle.SetActive(true);
                break;
            case 2:
                gShotgun.SetActive(true);
                break;
            case 3:
                gMusket.SetActive(true);
                break;
        }
    }
}
