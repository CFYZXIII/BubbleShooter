using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    LineRenderer line;
    List<Vector3> points;
    public GameObject leftWall, rightWall, upWall, downWall;
    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetComponent<LineRenderer>();
        points = new List<Vector3>();
        points.Add(new Vector2(leftWall.transform.position.x,downWall.transform.position.y));
        points.Add(new Vector2(leftWall.transform.position.x, upWall.transform.position.y));
        points.Add(new Vector2(rightWall.transform.position.x, upWall.transform.position.y));
        points.Add(new Vector2(rightWall.transform.position.x, downWall.transform.position.y));
        points.Add(new Vector2(leftWall.transform.position.x, downWall.transform.position.y));
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }


}
