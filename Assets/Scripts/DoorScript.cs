using UnityEngine;
using UnityEngine.UI;

/**
 * DOOR SCRIPT 
 * 
 * This script is used to control what the text outputs to the game screen once
 * key objects have been picked up.
 * 
 * @author: Jack Durnford
 **/

public class DoorScript : MonoBehaviour
{

    TextMesh _keysPickedUp; //Game object reference of TextMesh Object.
    public GameObject Player; //Game object reference of the Player object.

    /// <summary>
    /// Start method is run once when the object is instanciated.
    /// In this instance, the method is used to search for the object containing the
    /// "KeyCol" tag within Unity. Once found, a reference of the object is stored within
    /// "_keysPickedUp".
    /// </summary>
    void Start()
    {
        _keysPickedUp = (TextMesh)GameObject.FindGameObjectWithTag("KeyCol").GetComponent(typeof(TextMesh));        
    }

    /// <summary>
    /// Method run once per frame.
    /// Used to check how many keys the player has picked up.
    /// Switch statement used to change the TextMesh text depending on how many keys have
    /// been picked up.
    /// </summary>
    void Update()
    {
        switch (PlayerController.KeyCollected)
        {
            case 1:
                _keysPickedUp.text = "Key parts collected: 1/3";
                break;
           case 2:
                _keysPickedUp.text = "Key parts collected: 2/3";
                break;
            case 3: 
                _keysPickedUp.text = "Key parts collected: 3/3 - Get to the door!!";
                break;
        }

    }
}