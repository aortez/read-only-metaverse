using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update.
    void Start()
    {
        
    }

    // Update is called once per frame.
    void Update()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        LineRenderer line = GetComponent<LineRenderer>();

        // Draw a circle.
        Vector3[] positions = new Vector3[16];
        float angle = 0.0f;
        float radius = collider.radius;
        float red = 0;
        float blue = 0;
        float green = 1;
        float alpha = 1;
        Color startColor = new Color(red, green, blue, alpha);
        Color endColor = new Color(red, green, blue, alpha);
        line.startColor = startColor;
        line.endColor = endColor;

        float width = 1;
        line.startWidth = width;
        line.endWidth = width;

        // Reserve one for the extra line.
        float delta_angle = (2 * Mathf.PI) / (positions.Length - 1);
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = new Vector3(Mathf.Cos(angle) * (radius - width * 0.5f), Mathf.Sin(angle) * (radius - width * 0.5f), 0.0f);
            angle += delta_angle;
        }

        line.positionCount = positions.Length;
        line.loop = true;

        line.SetPositions(positions);
    }
}
