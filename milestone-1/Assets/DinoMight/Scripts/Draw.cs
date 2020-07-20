using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public GameObject brushPrefab;
    public Collider2D border;

    GameObject currentLine;
    LineRenderer lineRenderer;
    Color color = Color.black;

    List<Vector2> mousePositions = new List<Vector2>();
    int currentOrder = 1;
    float width = 0.05f;

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
        lineRenderer.material.SetColor("_Color", color);
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        currentOrder++;
        lineRenderer.sortingOrder = currentOrder;
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

    public void ChangeColor(string setColor)
    {
        switch (setColor)
        {
            case "black":
                color = Color.black;
                break;
            case "gray":
                color = Color.gray;
                break;
            case "blue":
                color = Color.blue;
                break;
            case "cyan":
                color = Color.cyan;
                break;
            case "green":
                color = Color.green;
                break;
            case "yellow":
                color = Color.yellow;
                break;
            case "red":
                color = Color.red;
                break;
            case "magenta":
                color = Color.magenta;
                break;
            case "white":
                color = Color.white;
                break;
            default:
                break;
        }
    }

    public void ClearCanvas()
    {
        GameObject[] drawings = GameObject.FindGameObjectsWithTag("Drawing");
        foreach (GameObject drawing in drawings)
        {
            Destroy(drawing);
        }
    }

    public void ChangeWidth(float newWidth)
    {
        width = newWidth;
    }
}
