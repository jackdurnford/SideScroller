using UnityEngine;
using System.Collections;

/**
 * SPEAR SCRIPT
 * This script is used to control movement of the Spear object, so that it can shoot
 * in the direction towards the zombie at the end of the level. Additionally, this
 * script script runs the method which starts the animation for flipping the Lever
 * switch.
 *
 * @author: Jack Durnford
 **/

public class SpearScript : MonoBehaviour
{ 

    public Rigidbody rb; //Reference to the rigidbody (physics controller) of the Spear.
    private GameObject anim; //Reference to the animation component of the Lever object. 
 
    /// <summary>
    /// The start method is run once when the object its attached to is instantiated. 
    /// Its job is to set the "rb" variable to reference the rigidbody of the Spear.
    /// Secondly, it sets the animation controller to the "anim" variable.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GameObject.FindWithTag("Lever2");
    }

    /// <summary>
    /// This method is used to shoot out the spear at a high speed using 
    /// "transform.right *200".
    /// It afterwards runs the Play animation attached to the animation controller
    /// of the Level switch object.
    /// </summary>
    public void Shoot()
    {
        rb.velocity = transform.right * 200;
        anim.GetComponent<Animation>().Play("FlipRight");
    }
}

