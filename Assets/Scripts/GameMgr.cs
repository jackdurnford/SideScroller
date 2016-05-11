using UnityEngine;

/**
 * GAME MANAGER SCRIPT
 * 
 * This script is used to detect whether the player is dead, and respawn the player at a checkpoint location. 
 * The script is also responsible for printing out respawn text and setting the cameras target.
 * 
 * @author: Jack Durnford
 **/

public class GameMgr : MonoBehaviour
{
    //Components
    private CameraObj _cam;

    //System
    private Vector3 _checkpoint;
    private TextMesh _textRespawn;
    public static bool Spawned;

    //Game Object references
    public GameObject CurrentPlayer; 
    public GameObject Player; 
    public GameObject RespawnText;

    /// <summary>
    /// The start method is run once when the object its attached to is instantiated. 
    /// The behaivor this method implements is that it will assign a refernece to the variables
    /// setup of the camera object. 
    /// It will then run the "SpawnPlayer" method, with the "_checkpoint" coordinates (Vector3 is
    /// coordinates within 3D world space).
    /// It will then look for the game object with the tag "RespawnT", which is the respawn text, 
    /// and store it within "textRespawn".
    /// </summary>
    private void Start()
    {
        _cam = GetComponent<CameraObj>();
        SpawnPlayer(_checkpoint);
        _textRespawn = (TextMesh) GameObject.FindGameObjectWithTag("RespawnT").GetComponent(typeof (TextMesh));
    }

    /// <summary>
    /// Used to instanciate/create the player object within the game world.
    /// Cam object's target is set to the instanciated player object.
    /// The player is spawned into the position of the coordinates passed in from "spawnPos".
    /// </summary>
    /// <param name="spawnPos">The last check point coordinates (X, Y and Z)</param>
    private void SpawnPlayer(Vector3 spawnPos)
    {
        CurrentPlayer = Instantiate(Player, spawnPos, Quaternion.identity) as GameObject;
        _cam.SetTarget(CurrentPlayer.transform);
        Spawned = true;
    }

    /// <summary>
    /// This method is run once per frame.
    /// The method is used to first check if a player is spawned, if it is - the text object
    /// will print a message. If the player is spawned, it will print a blank message. 
    /// If there is no player object within the game world, the player can press the respawn 
    /// button, which is "G", this will run the SpawnPlayer method above, with the coordinates
    /// set within "_checkpoint".
    /// </summary>
    private void Update()
    {
        if (!Spawned)
        {
            _textRespawn.text = "PRESS \"G\" TO RESPAWN";
        }

        if (Spawned)
        {
            _textRespawn.text = " ";
        }

        if (!CurrentPlayer)
        {
            if (Input.GetButtonDown("Respawn"))
            {
                SpawnPlayer(_checkpoint);
            }
        }
    }

    /// <summary>
    /// This method simply sets _checkpoint to whatever was passed into cp.
    /// </summary>
    /// <param name="cp"></param>
    public void SetCheckpoint(Vector3 cp)
    {
        _checkpoint = cp;
    }
}