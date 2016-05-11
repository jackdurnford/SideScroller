using UnityEngine;

/**
 * KEY SCRIPT
 * This script is used to control the animation of the key objects.
 * Because each key is of a different shape, I found it was useful to 
 * have some public rotation variables, which I could change per object
 * to allow different rotation. This is a form of polymorphism, as the 
 * code is being reused, but has different behaivors on each object.
 * 
 * @author: Jack Durnford
 **/

public class Key : MonoBehaviour
{
    public int XRotation = 0;
    public int YRotation = 0;
    public int ZRotation = 0;
    
    /// <summary>
    /// Update is called once per frame to continuously rotate the key objects in the directs set 
    /// in the Unity inspector window.
    /// </summary>
    private void Update()
    {
        transform.Rotate(XRotation*Time.deltaTime, YRotation*Time.deltaTime, ZRotation*Time.deltaTime);
            //rotates 50 degrees per second around an axis
    }
}