using UnityEngine;
using System.Collections;

/**
 * KEY SCRIPT
 * 
 * This script is used to detect when the key object collides with the player. 
 * The script then increments the number of keys the player has collected and 
 * deletes the key object.
 * 
 * @author: Jack Durnford
 **/

public class KeyScript : MonoBehaviour
{
    public PlayerController Playercontrolla; //Used to reference the PlayerController script.

    /// <summary>
    /// This method is used to detect once the key object has collided with the player object.
    /// After collision, it destroys the key object and increments the "KeyCollected" variable 
    /// within the PlayerController script by 1.
    /// </summary>
    /// <param name="col">Key object</param>
    void onTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Key")
        {
            Destroy(col.gameObject);
            PlayerController.KeyCollected += 1;
        }
    }
}
