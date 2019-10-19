using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows players to run into items to pick them up. Also stores information about the item (in the item class) so that they can be added to the players inventory.
/// Attached to the collider of the pick up area.
/// </summary>
public class ItemPickUp : MonoBehaviour {

    BoxCollider itemRb;
    Item itemInfo;

	// Use this for initialization
	void Start () {
        //Finds the Item component in the parent object (the actual model).
        itemInfo = GetComponentInParent<Item>();
	}
	
    //When something collides with this
    private void OnTriggerEnter(Collider other)
    {
        //Check the tag to see if it is a player
        if (other.gameObject.tag == "Player")
        {
            //If so, get the sphere collider of the parent of this and set the GameObject to inactive.
            SphereCollider parent = this.gameObject.GetComponentInParent<SphereCollider>();
            parent.gameObject.SetActive(false);
            //Check if the player has a general gun controller
            if (other.transform.GetChild(0).GetComponent<GunController>() != null)
            {
                //If it does, store the gunController in the playerInven variable
                GunController playerInven = other.transform.GetChild(0).GetComponent<GunController>();
                //Then add the gun to the inventory based on the itemID.
                playerInven.AddGun(itemInfo.GetItemID());
            }
            //Checks if the player has a keyboard controller. Previously used for testing. Outdated.
            else if (other.transform.GetChild(0).GetComponent<KeyboardGunController>() != null)
            {
                KeyboardGunController playerInven = other.transform.GetChild(0).GetComponent<KeyboardGunController>();
                playerInven.AddGun(itemInfo.GetItemID());
            }
        }
    }
}
