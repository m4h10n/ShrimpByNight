using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    private Enemy enemy;
    public float moveSpeed = 1.0f;
    public Transform[] waypoints = {};

    private Rigidbody2D m_RigidBody;
    int m_CurrentWaypointIndex;

    void Start ()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    void FixedUpdate ()
    {
		if (waypoints.Length == 0) return;
        Transform currentWaypoint = waypoints[m_CurrentWaypointIndex];
		if (currentWaypoint == null) return;

        Vector3 currentToTarget = currentWaypoint.position - (Vector3) m_RigidBody.position;

        if (enemy.getGameState() == Enemy.GameState.Wander)
        {
            if (currentToTarget.magnitude < 0.1f)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                enemy.EnemyBehavior = Enemy.GameState.Idle;
                enemy.setIdleCounter(-1f);
            }
            
            enemy.setAimDir((currentWaypoint.position - transform.position).normalized);
            //enemy.GetComponent<FieldOfView>().SetAimDirection((currentWaypoint.position - transform.position).normalized);
            m_RigidBody.MovePosition((Vector3) m_RigidBody.position + currentToTarget.normalized * moveSpeed * Time.deltaTime);
        }
        
    }

}