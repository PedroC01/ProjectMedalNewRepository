using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCar : MonoBehaviour
{
    public Transform[] startingPoints;      // Array of starting positions for the car
    public Transform[] endingPoints;        // Array of ending positions for the car
    public float speed = 1f;                // Speed of the car movement
    public List<Material> materialsList;    // List of materials to assign
    public GameObject objectToChangeColor;  // Object to change the color

    private int currentIndex = 0;           // Current index in the arrays for the car's movement
    private Vector3 targetPosition;         // Target position for the car to move towards

    void Start()
    {
        // Set the initial target position to the first ending point
        targetPosition = endingPoints[currentIndex].position;
    }

    void Update()
    {
        // Move the car towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check if the car has reached the target position
        if (transform.position == targetPosition)
        {
            // Increment the current index
            currentIndex++;

            // Check if the current index is equal to the array length
            if (currentIndex == endingPoints.Length)
            {
                // Reset the current index to 0
                currentIndex = 0;
            }

            // Set the new target position based on the current index
            targetPosition = endingPoints[currentIndex].position;

            // Rotate the car by 180 degrees on the y-axis
            transform.Rotate(Vector3.up, 180f);

            // Change the position of the car to the new starting point
            transform.position = startingPoints[currentIndex].position;

            // Change color of the specific object
            Renderer objectRenderer = objectToChangeColor.GetComponent<Renderer>();
                int i =Random.Range(0, materialsList.Count);
                objectRenderer.material = materialsList[i];
            
        }
    }
}
