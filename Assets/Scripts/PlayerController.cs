using System.Collections;
using UnityEngine;

/**
 * PLAYER CONTROLLER SCRIPT
 * 
 * This script is used to control the user input.
 * The script will take the user input and send it over to the PlayerPhysics class 
 * in order to create movement for the character.
 * 
 * @author: Jack Durnford
 **/

[RequireComponent(typeof (PlayerPhysics))] //Enhances encapsulation by making this script unusable unless PlayerPhysics script is attached to same object.

public class PlayerController : MonoBehaviour
{
    public static int KeyCollected; //Stores the amount of key parts the player has picked up.
    public static bool Death; //Used to trigger the player death animation.
    public static bool GameOver; //When 3 keys are collected, this bool allows player to complete the level.

    //System
    private int _jumpCount; //The amount of jumps the player has performed.
    private GameMgr _manager; //Reference to the GameMgr script.
    private Vector2 _moveAmount; //Vector2 varaible, to store how many units the character should move in any direction.
    private float _movementDirX; //Used to store the direction the player is facing on the X axis (-1.0 or 1.0).
    private bool _wallHolding; //Used to check whether the character is holding onto a wall or not. 
    private float _targetSpeed; //Used to store the speed at which the character will increment towards.
    private float _currentSpeed; //Used to store the current speed of the character.

    //Components 
    private PlayerPhysics _playerPhysics; //Used to store a reference to the PlayerPhysics componenet.
    private Animator _animator; //Used to store a reference to the Animator componenet of the player.

    //Player Handling
    public float CharGravity = 20; //Used to modify how quickly the player drops in world space.
    public float JumpingHeight = 8; //Sets the height at which the character jumps.
    public float Momentum = 50; //Sets the accelleration of the characters movement.
    public float Speed = 8; //Sets the speed of the characters movement.

    /// <summary>
    /// The start method is run once when the object its attached to is instantiated. 
    /// The behaivor this method implements is that it will first ensure the players Death
    /// boolean to false when it is spawned. 
    /// It will then assign the components of the player, for animation, player physics and
    /// the game manager to their corresponding variables.
    /// </summary>
    private void Start()
    {
        Death = false; //When The Player First Spawns We Need To Make Sure That Death Is False

        _playerPhysics = GetComponent<PlayerPhysics>();
        _animator = GetComponentInChildren<Animator>();
        _manager = Camera.main.GetComponent<GameMgr>();
    }

    /// <summary>
    /// The Update method is run every frame whilst the game is running, running all of the behaiver 
    /// inside continuously. 
    /// This method contains all the logic behind the player movement. Some of the key aspects of it are:
    /// Checking whether player is on the ground - If it is, it will stop movement on the Y axis, and reset 
    /// the jumpCount variable. If player is not on the ground, it will check if he is climbing, if he is, 
    /// the wallHolding variable will be set to true, enabling the player to grab onto the wall, and then 
    /// the climbing animation will activate.
    /// When the player presses a jump key (spacebar), it will perform a jump action, along with its animation
    /// whilst the player is still alive. It also checks whether the player has jumped twice or not before 
    /// hitting the ground.
    /// The method then detects which direction the player is in, and rotates it accordingly. 
    /// It then checks whether the player is wallclimbing, if he is, it will stop movement on the X axis
    /// and allow the player to slide down when holding the down key.
    /// The method then runs two lines of code which send the movement information picked up from the users
    /// key presses, to the PlayerPhysics script, in order to initialise movement of the player object.
    /// </summary>
    private void Update()
    {
        PlayerDeath();
        MomentumReset();
        KeysCollected();

        if (_playerPhysics.Grounded)
        {
            _moveAmount.y = 0; //Resets gravity to avoid gravity build up
            _jumpCount = 0;

            if (_wallHolding)
            {
                _wallHolding = false;
                _animator.SetBool("Wall Hold", false);
            }
        }
        else
        {
            //If wall holding
            if (!_wallHolding)
            {
                if (_playerPhysics.CanWallHold)
                {
                    _wallHolding = true;
                    _animator.SetBool("Wall Hold", true);
                }
            }
        }

        //Jump Input
        if (Input.GetButtonDown("Jump"))
        {
            if (!Death) //Prevents Player From Jumping While Dead

            {
                if (_playerPhysics.Grounded || _wallHolding)
                {
                    _moveAmount.y = JumpingHeight; //Makes the player jump the amount of units set in JumpHeight varaible, on the Y axis.
                    _animator.SetTrigger("Jump");
                    _animator.SetBool("Wall Hold", false);

                    if (_wallHolding)
                    {
                        _wallHolding = false;
                    }
                }
                else
                {
                    _animator.SetTrigger("Jump"); //Allows The Jump Animation To Play Again For Stationary Double Jump
                }

                if (!_wallHolding && _jumpCount < 2)  //If player has jumped twice and not climbing
                {
                    _moveAmount.y = JumpingHeight;
                    _jumpCount++;
                }
            } 
        }

        _animator.SetFloat("Speed", _currentSpeed);

        // Face direction
        if (_movementDirX != 0 && !_wallHolding)
        {
            transform.eulerAngles = (_movementDirX > 0) ? Vector3.up*180 : Vector3.zero;
        }

        //Set amount to move
        _moveAmount.x = _currentSpeed;

        if (_wallHolding)
        {
            _moveAmount.x = 0;
            if (Input.GetAxisRaw("Vertical") != -1)
                //If player holding down key, normal gravity applied, otherwise little amount of gravity applied.
            {
                _moveAmount.y = 0;
            }
        }

        //MAIN MOVEMENT CODE
        _moveAmount.y -= CharGravity*Time.deltaTime; //Y position is constantly being subtracted by whats stored in the CharGravity variable, each frame (Time.deltatime).
        _playerPhysics.Move(_moveAmount*Time.deltaTime, _movementDirX); //Constantly running the move amount method, based on whats stored within the moveAmount Vector2 variable.
    }

    /// <summary>
    /// Checks how many keys the player has.
    /// Once 3 are collected, it will set the GameOver boolean to true, and allow the player 
    /// to complete the game once reaching the end door. 
    /// </summary>
    public void KeysCollected()
    {
        if (KeyCollected == 3)
        {
            GameOver = true;
        }
    }
    
    /// <summary>
    /// This method is reset the momentum gathered when moving the character upon hitting a collider.
    /// e.g when player hits top speed by holding a directional button, and he hits a wall, the accelleration
    /// will be reset. 
    /// </summary>
    public void MomentumReset()
    {
        //Reset momentum upon collision
        if (_playerPhysics.MovementStopped)
        {
            _targetSpeed = 0;
            _currentSpeed = 0;
        }
    }

    /// <summary>
    /// This method is to control the behavior of when the player dies. 
    /// Firstly, it checks whether the player is dead or not by passing an (if) statement on the 
    /// "Death" boolean. If the player is dead, the death animation will play, and a coroutine method will 
    /// play. The coroutine method allows the "destroyPlayer" method to be run after a short delay. Meaning that
    /// the death animation will first play in full before the player disapears. 
    /// If the player is not dead, it will store some of the key movement variables. 
    /// </summary>
    public void PlayerDeath()
    {
        if (Death)
        {
            _animator.SetTrigger("Death"); 
            StartCoroutine(DestroyPlayer()); 
        }

        if (!Death)
        {
            _movementDirX = Input.GetAxisRaw("Horizontal"); //Directional key presses store +1 or -1 into variable.
            _targetSpeed = _movementDirX * Speed; //Sets the speed which the characters movement aims for.
            //Runs a method which increments the current speed of the character towards the target speed, and then stores it within "_currentSpeed"
            _currentSpeed = IncrementTowards(_currentSpeed, _targetSpeed, Momentum); 
        }
        else
        {
            _currentSpeed = 0; //When the player dies, their movement stops

        }

    }

    /// <summary>
    /// This method is used to smoothly increment the current speed
    /// of the player to the target speed.
    /// </summary>
    /// <param name="n">Current speed of the character</param>
    /// <param name="target">Target speed of the character</param>
    /// <param name="a">Acceleration of incrementing</param>
    /// <returns>Return a float value which will be the currentSpeed of the character</returns>
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target) //If current speed is equal to target speed.
        {
            return n; //Just return current speed.
        }
        float dir = Mathf.Sign(target - n); //Checks direction. Returns +1 or -1.
        n += a*Time.deltaTime*dir; //Increment current speed x accelleration x direction
        if (dir == Mathf.Sign(target - n)) 
        {
            return n;
        }
        else { 
        return target;
         }
    }

    /// <summary>
    /// This IEnumerator method allows the execution of the code to yeild the WaitForSeconds function,
    /// which job is to tell the script to simply wait a specified amount of seconds.
    /// The methods job is to wait 2 and a half seconds, and then delete the character object. 
    /// This allows enough time for the character death animation to play. 
    /// </summary>
    private IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(2.5f);
        GameMgr.Spawned = false;
        Destroy(gameObject);
    }

    /// <summary>
    /// This method is to check when the player collides with a trigger. 
    /// In this case, the method is looking for collision with key objects,
    /// check point triggers and the end door trigger.
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Checkpoint")
        {
            _manager.SetCheckpoint(col.transform.position);
        }

        if (col.gameObject.tag == "Key")
        {
            Destroy(col.gameObject);
            KeyCollected++;
            Debug.Log("ADDED A KEY, OR TRIED");
        }

        if (col.gameObject.tag == "TheEnd")
        {
            if (KeyCollected == 3)
            {
                Application.LoadLevel("Menu"); //Sets the scene to the main menu.
            }
        }
    }
}