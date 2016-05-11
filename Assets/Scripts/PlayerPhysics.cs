using UnityEngine;
using System.Collections;

/**
 * PLAYER PHYSICS SCRIPT
 * 
 * This script is used to control all the physics and collisions for the player object and functions in conjuction with 
 * the PlayerController script.
 * 
 * NOTE: For better understanding, I've included comments on all variables, block comments for blocks of code, and then
 * line comments for key/important lines of code.
 * 
 * @author: Jack Durnford
 **/

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour
{
    //System
    private Vector3 _size;  //Vector3 variable used to store the size of the players box collider.
    private Vector3 _centre; //Vector3 variable used to store the center position of the players box collider.
    private Ray _ray; //Used to store the origin of the ray, and the direction of the ray.
    private RaycastHit _hit; //Used to store the information of colliders the raycast has hit.
    public LayerMask CollisionMask; //Used to store information of a layer within Unity. 

    //Components 
    private BoxCollider _collider; //Used to store a reference to the box collider compononet of the player.

    //Player Handling
    private const float _SKIN = .005f; //Adds tiny gap between floor and player so that raycast doesnt go through ground.
    private const int _RAYCASTDIVISION_X = 3; //This sets the amount of divisions between each raycast shooting along the X axis.
    private const int _COLLISION_DIVISION_Y = 10; //Raycast divisions along the Y axis.

    [HideInInspector] //HIDES VARIABLE FROM THE INSPECTOR WINDOW IN UNITY, SO THAT IT CANT BE CHANGED.
    public bool Grounded; //Stores whether the character is on the ground or not.

    [HideInInspector]
    public bool MovementStopped; //Used to detect whether player is moving or not.

    [HideInInspector]
    public bool CanWallHold; //Used to check whether the player is able to wall hold.

    /// <summary>
    /// This method is run once when object it is attached to (player) is instantiated. 
    /// Its use is to store the box collider information within variables.
    /// </summary>
    private void Start()
    {
        _collider = GetComponent<BoxCollider>(); 
        _size = _collider.size;
        _centre = _collider.center;
    }

    
    /// <summary>
    /// This method controls the movement physics for the player object.
    /// The first sections of the code create and control the raycasts,
    /// which will be used for collision purposes. It does this by shooting out
    /// rays for each side of the character, depending on which direction being 
    /// moved in. It uses a for loop to draw out the rays multiple times.The amount
    /// of rays being shot out depends on the size of the box collider.
    /// It then runs an if statement which checks whether the rays hit a collider, 
    /// so it can then stop the players movement.
    /// </summary>
    /// <param name="moveAmount">Amount to move in a particular direction.</param>
    /// <param name="moveDirX">Direction player moving in, picked up from keyboard input.</param>
    public void Move(Vector2 moveAmount, float moveDirX)
    {
        var deltaY = moveAmount.y; //How many units to move along Y axis
        var deltaX = moveAmount.x; //How many units to move along X axis

        Vector2 p = transform.position; //Players current position

    //Check collisions ABOVE and BELOW
        Grounded = false;
        for (int i = 0; i < _RAYCASTDIVISION_X; i++)
        {
            float dir = Mathf.Sign(deltaY); //Return positive or negative value depending on direction
            //Left, centre and then rightmost point of the collider 
            float x = (p.x - _size.x / 2) + _size.x / (_RAYCASTDIVISION_X - 1) * i; 
            float y = p.y + _size.y / 2 * dir; //Bottom of the collider

            _ray = new Ray(new Vector2(x, y), new Vector2(0, dir)); 
            Debug.DrawRay(_ray.origin, _ray.direction);

            if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaY) + _SKIN, CollisionMask))
            {
                float dst = Vector3.Distance(_ray.origin, _hit.point);

                if (dst > _SKIN) //Stop player's downwards movement after coming within skin width of a collider
                {
                    deltaY = dst * dir - _SKIN * dir;
                }
                else
                {
                    deltaY = 0;
                }
                Grounded = true;
                break; //if raycast hit the ground, nothing more needs to be tested, so loop is stopped.
            }
        }

        //Check collisions LEFT and RIGHT
        MovementStopped = false;
        CanWallHold = false;

        if (deltaX != 0)
        {
            for (int i = 0; i < _COLLISION_DIVISION_Y; i++)
            {
                float dir = Mathf.Sign(deltaX);
                float x = p.x + _centre.x + _size.x / 2 * dir; //Left, centre and then rightmost point of the collider
                float y = p.y + _centre.y - _size.y / 2 + _size.y / (_COLLISION_DIVISION_Y - 1) * i;

                _ray = new Ray(new Vector2(x, y), new Vector2(dir, 0)); //Creates a new ray at origin set above, and direction going left or right.
                Debug.DrawRay(_ray.origin, _ray.direction);

                if (Physics.Raycast(_ray, out _hit, Mathf.Abs(deltaX) + _SKIN, CollisionMask)) //If raycast hit collider
                {

                    if (_hit.collider.tag == "Wall Jump")
                    {
                        if (Mathf.Sign(deltaX) == Mathf.Sign(moveDirX) && moveDirX != 0) //If the direction the character is moving is equal to the direction he is facing
                        {
                            CanWallHold = true;

                        }
                    }

                    float dst = Vector3.Distance(_ray.origin, _hit.point); //Get distance between player and ground

                    if (dst > _SKIN) //Stop player's downwards movement after coming within skin width of a collider
                    {
                        deltaX = dst * dir - _SKIN * dir;
                    }
                    else
                    {
                        deltaX = 0;
                    }
                    MovementStopped = true;
                    break; //if raycast hit the ground, nothing more needs to be tested, so loop is stopped.
                }
            }


        }

        if (!Grounded && !MovementStopped)
        {
            //Collisions for DIAGONAL directions
            Vector3 playerDir = new Vector3(deltaX, deltaY);
            Vector3 o = new Vector3(p.x + _centre.x + _size.x / 2 * Mathf.Sign(deltaX),
                p.y + _centre.y + _size.y / 2 * Mathf.Sign(deltaY));
            Debug.DrawRay(o, playerDir.normalized); //Draw test rays
            _ray = new Ray(o, playerDir.normalized.normalized);

            //Detects when a ray hits a collider. 
            if (Physics.Raycast(_ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), CollisionMask))
            {
                Grounded = true;
                deltaY = 0;
            }
        }

        Vector2 finalTransform = new Vector2(deltaX, deltaY);
        transform.Translate(finalTransform, Space.World);
    }
}




