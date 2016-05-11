using UnityEngine;

/**
 * CHECK POINT SCRIPT
 * 
 * This script is used to draw a box gizmo, based upon the size of the box collider component. 
 * 
 * @author: Jack Durnford
 **/

[RequireComponent(typeof (BoxCollider))]
public class BoxGizmos : MonoBehaviour
{
    public Color gizmoColour; //Stores a colour for the Gizmo

    /// <summary>
    /// Method is used to draw the cube with a specified colour, with the size of its box collider.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColour;
        Gizmos.DrawCube(transform.position + GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);
    }
}