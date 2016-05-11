using UnityEngine;
using System.Collections;

/**
 * ACID DELETE SCRIPT
 * 
 * This script is used to detect once the "AcidDelete" object has collider with the acid droplet object
 * 
 * @author: Jack Durnford
 **/

public class AcidDelete : MonoBehaviour {

    /// <summary>
    /// Once the acid droplet collides with the acid deleting object, the droplet will be deleted.
    /// </summary>
    /// <param name="col">Acid droplet</param>
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "AcidDelete")
		{
			Destroy(gameObject);
		}

	}
			

}
