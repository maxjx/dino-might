using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject brushPrefab;
    public Collider2D border;

    GameObject currentLine;
    LineRenderer lineRenderer;

    List<Vector2> mousePositions = new List<Vector2>();

    // Update is called once per frame
    void Update()
    {
        Vector2 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (border.bounds.Contains(tempMousePos))
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButton(0))
            {
                if (Vector2.Distance(tempMousePos, mousePositions[mousePositions.Count - 1]) > .1f)
                {
                    UpdateLine(tempMousePos);
                }
            }
        }
    }

    void CreateLine()
    {
        currentLine = Instantiate(brushPrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        mousePositions.Clear();
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, mousePositions[0]);
        lineRenderer.SetPosition(1, mousePositions[1]);
    }

    void UpdateLine(Vector2 newMousePos)
    {
        mousePositions.Add(newMousePos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newMousePos);
    }
}
