using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    private PathfindingGrid grid;
    public float moveSpeed = 5f;
    private List<Node> path;
    private int targetIndex = 0;
    private float pathUpdateInterval = 0.2f;
    private float lastPathUpdate = 0f;
    public float stoppingDistance = 0.5f;
    public bool debugPath = true;

    void Start()
    {
        grid = FindObjectOfType<PathfindingGrid>();
        if (grid == null)
        {
            Debug.LogError("PathfindingGrid not found in the scene!");
        }
    }

    void Update()
    {
        if (player == null || grid == null) return;

        // Update path periodically
        if (Time.time - lastPathUpdate > pathUpdateInterval)
        {
            SetTarget(player.position);
            lastPathUpdate = Time.time;
        }

        FollowPath();
    }

    void FollowPath()
{
    if (path == null || targetIndex >= path.Count) return;

    Vector3 targetPosition = path[targetIndex].worldPosition;
    
    // Calculate direction for both X and Z axes
    float dx = targetPosition.x - transform.position.x;
    float dz = targetPosition.z - transform.position.z;
    Vector3 movement = new Vector3(dx, 0, dz).normalized;

    // Calculate distance to target using both X and Z
    float distanceToTarget = Vector3.Distance(
        new Vector3(transform.position.x, 0, transform.position.z),
        new Vector3(targetPosition.x, 0, targetPosition.z)
    );

    // Move if not at stopping distance
    if (distanceToTarget > stoppingDistance)
    {
        // Apply movement to both X and Z axes
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Only rotate if we're actually moving
        if (movement.magnitude > 0.01f)
        {
            transform.forward = movement;
        }
    }

    // Move to next node when close enough
    if (distanceToTarget < 0.1f)
    {
        targetIndex++;
    }
}
    public void SetTarget(Vector3 target)
    {
        if (grid == null) return;
        path = grid.GetPath(transform.position, target);
        targetIndex = 0;
    }

    void OnDrawGizmos()
    {
        if (!debugPath || path == null) return;
        
        // Draw path
        for (int i = targetIndex; i < path.Count - 1; i++)
        {
            Gizmos.color = Color.yellow;
            Vector3 start = path[i].worldPosition;
            Vector3 end = path[i + 1].worldPosition;
            // Draw lines at a consistent height
            start.y = transform.position.y;
            end.y = transform.position.y;
            Gizmos.DrawLine(start, end);
        }
    }
}