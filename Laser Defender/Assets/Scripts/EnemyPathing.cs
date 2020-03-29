/* This class controls the enemy's pathing system
 * 
 * There are an X amount of waypoints that the enemy can go to
 * 
 * The waves are configured in the WaveConfig script as scriptable objects
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Information about the EnemyPathing system
    WaveConfig waveConfig;
    List<Transform> waypoints;
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
        CheckCurrentWaypoint();
    }

    // Checks to make sure that we are not on the last waypoint and if we're not the enemy will move
    private void CheckCurrentWaypoint()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            Move();
        }
    }

    // Setters set values and getters get them
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        // When we use the this keyword we are referring to the script itself
        // So this.waveConfig is referencing the waveConfig variable up at he top of the EnemyPathing script
        // This is useful when using variables of the same name (like the parameter of this function)
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        // Where we want to go to
        var targetPosition = waypoints[waypointIndex].transform.position;

        // How fast we want to move
        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        // If you've made it to the target position head to the next one
        if (transform.position == targetPosition)
        {
            waypointIndex++;
        }
        
        // If you've reached all positions then destroy yourself (I might change this functionality later)
        if (waypointIndex == waypoints.Count)
        {
            Destroy(gameObject);
        }
    }
}
