using UnityEngine;

/**
 * ENEMY MOVEMENT SCRIPT
 * 
 * This script is used to control behaiver for the zombie objects.
 * Takes care of movement and collision triggers.
 * 
 * @author: Jack Durnford
 **/

public class EnemyMovement : MonoBehaviour
{
    private bool _spawned = false; //Used to check whether zombie has already been re-spawned after being shot by spear.
    private bool _dead = false; //To check whether the zombie has been shot by the spear.
    private Animator _anim; //Reference to the zombie animation controller.

    public float DownwardForce; //Gravity force.
    public float MoveSpeed; //Zombie movement speed.

    public GameObject Spear; //Reference to spear object.
    public GameObject Zombie; //Reference to the Zombie.
    public GameObject ZombieCol; //Reference to walking zombie.
    public GameObject ZombieRag; //Reference to zombie to be instanciated.

    /// <summary>
    ///     Run once per frame to move the zombie object along the X axis.
    ///     If the zombie is alive, he will move.
    ///     If the zombie is dead, a new zombie is spawned ontop of the dead one with a new "struggling" animation,
    ///     the dead one will be deleted.
    /// </summary>
    private void Update()
    {
        Debug.Log(_dead);
        if (_dead == false)
        {
            transform.Translate(new Vector3(MoveSpeed, DownwardForce, 0) * Time.deltaTime);
        }
        if (_dead == true)
        {
            if (_spawned == false)
            {
                Instantiate(ZombieRag, new Vector3(0, 0, 0), Quaternion.Euler(360, 83, 358));

                Destroy(ZombieCol);
                Destroy(GameObject.Find("Enemy 1"));
            }
        }
    }

    /// <summary>
    ///     Used to detect collision with an objects with specific tags.
    ///     When object with "Spear" tag is hit: Sets whether the zombie is dead to "True". Then it sets the position of the
    ///     zombie, to that of the Spear object.
    ///     When object with "RockG" tag is hit: It switches the layer of the Rock object, aiding collision with the player.
    /// </summary>
    /// <param name="col">Collided object reference</param>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Spear")
        {
            _dead = true;
            transform.parent = Spear.transform;
        }

        if (col.gameObject.tag == "RockG")
        {
            gameObject.layer = 8;
        }
    }

    /// <summary>
    ///     Used to detect collision with an objects with specific tags.
    ///     When object with "Block" tag is hit: The zombie will switch movement direction.
    /// </summary>
    /// <param name="col">Collided object reference</param>
    private void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Block")
        {
            MoveSpeed *= -1;
            Zombie.transform.localEulerAngles = new Vector3(0, Zombie.transform.localEulerAngles.y - 180, 0);
        }
    }
}