using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public bool isRender = false;

    public float distance;
    public float alpha = Mathf.PI / 2;
    public int general;
    public float beta = 0;
    LineRenderer line;
    List<Vector3> points;
    int reflectionCount = 4;
    int currentReflecton = 0;
    public LayerMask layer_mask;

   





    private void Start()
    {

        line = transform.GetComponent<LineRenderer>();
        points = new List<Vector3>();



    }

    private void FixedUpdate()
    {
      
    }


    public void drawLine()
    {
        

        points.Clear();
        currentReflecton = 0;
        points.Add(transform.position);


        Vector2 fin = transform.parent.position + new Vector3(Mathf.Cos(alpha + beta), Mathf.Sin(alpha + beta)) * distance;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, fin);
        if (hit)
        {
            if (hit.transform.tag == "RightWall" || hit.transform.tag == "LeftWall")
            {
                reflect(transform.position, hit);
               
            } else
            points.Add(hit.point);

        }
        else
        {
            points.Add(fin);
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());



    }

    public void reset()
    {
        points.Clear();

        line.positionCount = points.Count;

    }

    public void reflect(Vector2 startpos, RaycastHit2D hit)
    {
        if (currentReflecton < reflectionCount)
        {

            currentReflecton++;


            points.Add(hit.point);

            Vector2 inDirection = (hit.point - startpos).normalized;
            Vector2 newDirection = Vector2.Reflect(inDirection, hit.normal);


            RaycastHit2D newHit = Physics2D.Linecast(hit.point + (newDirection * 0.0001f), newDirection*distance, layer_mask);

            if (newHit)
            {
                if (newHit.transform.tag == "LeftWall" || newHit.transform.tag == "RightWall")
                {
                    reflect(newHit.point, newHit);

                }
                else
                { points.Add(newHit.point);
                   
                }
            }

           

        }
    }
}
