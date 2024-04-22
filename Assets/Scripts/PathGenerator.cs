using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] float centerCircleSize = 0.2f;

    List<Vector2> waypoints = new List<Vector2>();
    //EdgeCollider2D edgeCollider;

    EnemySpawner spawner;
    Camera mainCamera;

    private void Awake()
    {
        spawner = GetComponent<EnemySpawner>();
        if (spawner == null) { Debug.Log("spawner is NULL"); }
        mainCamera = Camera.main;
    }

    // This script should:
    // create a new parent game object 'PathObject'
    // create children game objects whos positions will create a path
    //   alternativly could just create line an array of postions... would need to modify other scripts...



    private void Start()
    {
        //edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        //edgeCollider.enabled = false;
        //UpdateWaveWithWaypoints();

    }

    public void UpdateWaveWithWaypoints(int idx)
    {
        GenerateWaypoints();
        spawner.SetWaypoints(idx,waypoints);
        //edgeCollider.SetPoints(waypoints);
    }

     void GenerateWaypoints()
    {
        waypoints = new List<Vector2>();
        // Point 1
        // Random point along perimeter of screen.
        waypoints.Add(mainCamera.ViewportToWorldPoint(GetPointOnScreenPerimeter(0)));
        // Point 2
        // Random point in a circle (oval) around center of screen
        waypoints.Add(mainCamera.ViewportToWorldPoint(GetPointAroundCenter()));
        // Point 3
        // Same as point one, random point along perimeter of screen
        waypoints.Add(mainCamera.ViewportToWorldPoint(GetPointOnScreenPerimeter(-1)));

    }


    Vector2 GetPointAroundCenter()
    {
        return NormalizePoint(Random.insideUnitCircle * centerCircleSize);
    }

    Vector2 GetPointOnScreenPerimeter(float yStartPoint)
    {
        // for starting waypoint we only points that are above player, maybe y>=0 
        Vector2 point = Vector2.zero;
        bool findPoints = true;
        while (findPoints)
        {
            point = Random.insideUnitCircle;
            if (point.y >= yStartPoint)
            {
                findPoints = false;
            }
        }        
        point = NormalizePoint(point);

        float screenOffset = 0.05f;
        if (Random.Range(0, 2) == 0)
        {
            point.x = Mathf.Round(point.x);
            if (point.x == 0) { point.x -= screenOffset; }
            else { point.x += screenOffset; }
        } else
        {
            point.y = Mathf.Round(point.y);
            if (point.y == 0) { point.y -= screenOffset; }
            else { point.y += screenOffset; }
        }
        return point;
    }
    Vector2 NormalizePoint(Vector2 point)
    {
        // need to normalize between 0 and 1, since a unit circle has values from -1 to 1
        point.x = (point.x + 1) / 2;
        point.y = (point.y + 1) / 2;
        return point;
    }

}
