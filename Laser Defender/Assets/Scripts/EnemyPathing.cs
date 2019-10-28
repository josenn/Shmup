using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(waypointIndex <= waypoints.Count - 1)
        {
            Move();
        }
    }

    private void Move()
    {
        //where we want to go to
        var targetPosition = waypoints[waypointIndex].transform.position;

        //how fast we want to move
        var movementThisFrame = moveSpeed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
        if (waypointIndex == waypoints.Count)
        {
            Destroy(gameObject);
        }
    }
}
