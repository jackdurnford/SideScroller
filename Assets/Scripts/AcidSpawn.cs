using UnityEngine;

/**
 * ACID SPAWNING SCRIPT 
 * 
 * This script is used repeatedly instantiate the acid droplet object. 
 * 
 * @author: Jack Durnford
 **/

public class AcidSpawn : MonoBehaviour
{
    public GameObject Acida;        //Reference to the acid drolet prefab.
    public GameObject AcidSpawner; //Reference to the spawning location of the AcidSpawner object.

    /// <summary>
    /// This method once the game starts, and calls an InvokeRepeating function.
    /// This function will repeatedly run the "SpawnEnemy" method every 1 second.
    /// </summary>
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 1);
    }

    /// <summary>
    /// Instantiates the acid droplet every 1 second at the position of the acid spawner object, and
    /// with a specified rotation.
    /// </summary>
    private void SpawnEnemy()
    {
        Instantiate(Acida, AcidSpawner.transform.position, Quaternion.Euler(270, 90, 0)); //spawn enemy starting in 1 second every 15 seconds
    }
}


