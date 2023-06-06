using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerpendicularCamera : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float lerpSpeed;
    public float distanceFromMidpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // Calculate the midpoint between the two players
        Vector3 midpoint = (player1.position + player2.position) * 0.5f;

        // Calculate the vector perpendicular to the line connecting the two players
        Vector3 perpendicular = Vector3.Cross(player2.position - player1.position, Vector3.up).normalized;

        // Calculate the desired camera position by adding the perpendicular vector to the midpoint
        Vector3 desiredCameraPosition = midpoint + perpendicular * distanceFromMidpoint;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredCameraPosition, lerpSpeed * Time.deltaTime);

        // Make the camera look at the midpoint
        transform.LookAt(midpoint);
    }
}
