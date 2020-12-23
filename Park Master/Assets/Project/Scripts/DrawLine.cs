using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    /*This code is for drawing on screen and save location of coordinates*/
    public LayerMask layers;
    public Camera cam = null;
    public LineRenderer lineRenderer = null;
    private Vector3 mousePos;
    private Vector3 Pos = new Vector3(10,10,10);
    private Vector3 previousPos;
    public List<Vector3> linePositions = new List<Vector3>();
    public float minimumDistance = 0.05f;
    private float distance = 0;

    Ray ray;
    RaycastHit hit;
    bool canGo = false;

    private void Start()
    {
        Manager.instance.ResetAll(); // refresh all setups
        linePositions.Clear();
        previousPos = Pos;
        linePositions.Add(Pos);
        lineRenderer.positionCount = linePositions.Count;
        lineRenderer.SetPositions(linePositions.ToArray());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  //when mouse clicked
        {
            Manager.instance.ResetAll(); // refresh all setups
            linePositions.Clear();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layers))
            {
                if (hit.collider.CompareTag("StartPoint"))
                {
                    Pos = hit.point;

                    Pos.y += 0.001f;
                    previousPos = Pos;
                    linePositions.Add(Pos); //add to line renderer
                    canGo = true;
                }
            }
            
        }
        else
        {
            if (Input.GetMouseButton(0) && canGo) // while pressing mouse button
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, layers))
                {
                    Pos = hit.point;
                    Pos.y += 0.001f;
                    distance = Vector3.Distance(Pos, previousPos);
                    if (distance >= minimumDistance) /// check wether it is close to last point or not
                    {
                        previousPos = Pos;
                        linePositions.Add(Pos);
                        lineRenderer.positionCount = linePositions.Count;
                        lineRenderer.SetPositions(linePositions.ToArray()); //set points
                    }
                }
                
            }
            else
            {
                if (Input.GetMouseButtonUp(0))
                {
                    canGo = false;
                    Manager.instance.followLine(linePositions); // if mouse button released begin to move.
                }
            }
        }
    }
}
