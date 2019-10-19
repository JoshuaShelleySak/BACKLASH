using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the bullet damage, collisions/bouncing and player damage.
/// </summary>
public class BulletController : MonoBehaviour {
    
    //Speed that the bullet will travel
    public float fBulletSpeed = 15;
    //How many times the bullet will bounce before being destroyed
    public int iBulletBounceNum = 4;
    //Damage that the bullets will do
    public int iDamageToGive = 10;
    //Size of the bullet that will be instantiated
    public float fBulletSize = 0.3f;

    //Who shot the bullet being instantiated
    public int iPlayerWhoShot;

    //Whether the gun that shot the bullet is the shotgun
    public bool bIsShotgun = false;
    
    //Makes sure the bullets only bounce off of objects it's supposed to
    private LayerMask wallCollision;
    //How many collisions have been done so far
    private int iCollisionCounter = 0;


    // Use this for initialization
    void Start () {
        //Gets the layer that the bullets will bounce off of 
        wallCollision = LayerMask.GetMask("WallCollision");
    }
	
	// Update is called once per frame
	void Update () {
        //Sets the bullet size
        this.transform.localScale.Set(fBulletSize, fBulletSize, fBulletSize);

        //Moves the bullet forwards over each frame by the bullet speed
        transform.Translate(Vector3.forward * Time.deltaTime * fBulletSpeed);
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //Checks if the raycast hit anything on the layer 
        if (Physics.Raycast(ray, out hit, Time.deltaTime * fBulletSpeed + .1f, wallCollision))
        {
            //If so, increase the collision counter
            iCollisionCounter++;
            //If the collision counter is now higher than the number of times the bullet should bounce, destroy the bullet
            if (iCollisionCounter >= iBulletBounceNum)
            {
                Destroy(gameObject);
            }
            //Rotates the bullet after it has hit a wall
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
        }
	}

    //When the bullet enters a collider
    private void OnCollisionEnter(Collision other)
    {
        //Check if the collider hit has the Player tag
        if (other.gameObject.tag == "Player")
        {
            //Finds the playerHealthManager of the player hit
            PlayerHealthManager phm = other.gameObject.GetComponent<PlayerHealthManager>();
            //If they are not currently in a invincibility frame
            if (phm.canBeDamaged)
            {
                //Damage the player and send who shot the bullet
                phm.HurtPlayer(iDamageToGive, iPlayerWhoShot);
            }
            //Destroys the bullet on impact
            Destroy(gameObject);
        }
    }
}
