using UnityEngine;

/**
 * This script is used to simply activate the "death" boolean in the "PlayerController" class.
 * 
 * @author: Jack Durnford
 **/

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
			PlayerController.Death = true;
        }
    }
}