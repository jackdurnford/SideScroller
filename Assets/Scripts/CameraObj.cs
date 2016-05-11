using UnityEngine;

/**
 * CAMERA OBJECT SCRIPT
 * 
 * This script is used to control camera object in world space.
 * Its main job is to get the position of the player, and then increment the cameras position
 * towards it.
 * 
 * @author: Jack Durnford
 **/

public class CameraObj : MonoBehaviour
{
    private const float _TRACK_SPEED = 20; //Speed at which camera follows
    private Transform _target; //Reference to Player object (the target)

    /// <summary>
    /// Set target to the player once its been created.
    /// </summary>
    public void SetTarget(Transform t)
    {
        _target = t;
    }

    /// <summary>
    /// LastUpdate, is run after all "Update" methods.
    /// This is used so that the camera will follow the player AFTER its moved.
    /// </summary>
    private void LateUpdate()
    {
        if (_target)
        {
            var x = IncrementTowards(transform.position.x, _target.position.x, _TRACK_SPEED);
            var y = IncrementTowards(transform.position.y, _target.position.y, _TRACK_SPEED);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    /// <summary>
    /// This method (copied from the PlayerController) is used to check whether
    /// the camera object is in line with the player object, if its not, it will
    /// increment the camera objects X/Y position value accordingly.
    /// </summary>
    /// <param name="n">Camera object X/Y position</param>
    /// <param name="target">Player object X/Y position</param>
    /// <param name="a">Acceleration for camera movement</param>
    /// <returns>X/Y position value</returns>
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target) //Is Current position is equal to target position?
        {
            return n;
        }
        var dir = Mathf.Sign(target - n); //Checks direction(returns +1 or -1).
        n += a*Time.deltaTime*dir; //Increment current speed x accelleration x direction.

        if (dir == Mathf.Sign(target - n)) //Still facing in same direction?
        {
            return n;
        }
        return target;
    }
}