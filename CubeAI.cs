using UnityEngine;

public class CubeAI : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float rayDistance = 1.5f;
    public LayerMask obstacleLayer;
    public LayerMask groundLayer;

    private Vector3 moveDirection;

    void Start()
    {
        // Set initial move direction
        moveDirection = transform.forward;
    }

    void Update()
    {
        MoveCube();
        AvoidObstacles();
        StayOnPlane();
    }

    void MoveCube()
    {
        // Move forward in the current direction
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    void AvoidObstacles()
    {
        // Cast a ray in the forward direction to detect obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, obstacleLayer))
        {
            // If an obstacle is detected, rotate to avoid it
            RotateCube();
        }
    }

    void RotateCube()
    {
        // Randomly change direction (left or right)
        float randomRotation = Random.Range(-90f, 90f);
        transform.Rotate(Vector3.up, randomRotation * rotationSpeed * Time.deltaTime);
        // Update move direction after rotation
        moveDirection = transform.forward;
    }

    void StayOnPlane()
    {
        // Cast a ray downwards to check if the cube is still on the plane
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance, groundLayer))
        {
            // If no ground is detected, rotate the cube to find a safe direction
            RotateCube();
        }
    }

    private void OnDrawGizmos()
    {
        // Draw raycasts for visualization in the scene view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * rayDistance);
    }
}
