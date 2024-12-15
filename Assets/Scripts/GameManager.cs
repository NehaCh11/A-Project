using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    void Start() {
        SpawnPlayer();
        SpawnEnemy();
    }

    void SpawnPlayer() {
        if (player == null) {
            player = GameObject.CreatePrimitive(PrimitiveType.Sphere); // Create player if not assigned
            player.transform.position = Vector3.zero; // Spawn player at origin
            player.name = "Player";
            player.AddComponent<PlayerController>(); // Add PlayerController component
        }
    }

    void SpawnEnemy() {
        if (enemyPrefab != null && spawnPoints.Length >=0) {
            // Spawn enemies at random spawn points
            foreach (var spawnPoint in spawnPoints) {
                GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                enemy.name = "Enemy";
                EnemyAI enemyAI = enemy.AddComponent<EnemyAI>();
                enemyAI.player = player.transform; // Assign player to the enemy's AI
            }
        } else {
            Debug.LogError("Enemy prefab or spawn points are not assigned.");
        }
    }
}
