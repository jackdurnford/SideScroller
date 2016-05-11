using UnityEngine;
using System.Collections;

/**
 * ZOMBIE DEAD SCRIPT
 * This script is used to control behaiver for the dead zombie object.
 * The behaiver being implmented here is to attach the new dead zombie
 * to the spear after the alive one was shot. 
 * 
 * @author: Jack Durnford
 **/

public class ZombieBiped : MonoBehaviour
{

    public GameObject Zombie; //Reference of the alive zombie object.

    /// <summary>
    /// Run once per frame.
    /// The Spear object is found within the game world, and a reference is created
    /// from it under "Spear". 
    /// The new dead zombie object's location is the same as the Spears object, giving 
    /// the illussion that its stuck to it.
    /// the dead one will be deleted.
    /// </summary>
    void Update()
    {
        GameObject Spear = GameObject.Find("Spear");

        Zombie.transform.position = Spear.transform.position;
    }
}
