using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAim : MonoBehaviour
{
    void Update()
    {
        // Get the position of the mouse in screen coordinates
        Vector3 mousePosition = Input.mousePosition;
        
        // Convert the screen point to a point in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        // Calculate the direction from the current object's position to the mouse position
        Vector3 direction = mouseWorldPosition - transform.position;
        direction.y = 0; // Optional: if you want to restrict rotation to the X-Z plane

        // Rotate the object to face the mouse direction
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
