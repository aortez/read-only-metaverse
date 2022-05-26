using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    const float valueMin = 0.01f;
    const float growth = 0.0001f;
    public float valueMax = 1.0f;
    public float value = 0.0f;

    public List<Synapse> outputs = new List<Synapse>();

    // Start is called before the first frame update.
    void Start()
    {
        // Make init conditions random.
        value = Random.Range(valueMin, valueMax);
    }

    void OnTriggerEnter(Collider other)
    {
        // foreach (ContactPoint contact in collision.contacts)
        // {
        //     Debug.DrawRay(contact.point, contact.normal, Color.white);
            Debug.Log("collision!");
        // }
    }

    // Update is called once per frame.
    void Update()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();

        LineRenderer line = GetComponent<LineRenderer>();

        // Draw the Neuron...
        Vector3[] positions = new Vector3[16];
        float angle = 0.0f;
        float radius = collider.radius;
        float spikeRatio = value / valueMax;
        float red = 0.1f + spikeRatio;
        float blue = 0.9f - red;
        line.startColor = new Color(red, 0, blue, 1.0f);
        line.endColor = new Color(red, 0, blue, 1.0f);
        float width = Mathf.Min(0.2f + spikeRatio * 0.8f, 1.0f);
        line.startWidth = width;
        line.endWidth = width;

        // Reserve one for the extra line.
        float delta_angle = (2 * Mathf.PI) / (positions.Length - 1);
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = new Vector3(Mathf.Cos(angle) * (radius - width * 0.5f), Mathf.Sin(angle) * (radius - width * 0.5f), 0.0f);
            angle += delta_angle;
        }
        // positions[positions.Length - 1] = new Vector3(0, 0, 0.0f);
        line.positionCount = positions.Length;
        line.loop = true;

        line.SetPositions(positions);

        // // Very primitive spiking mechanism...
        value += growth;
        if (value > valueMax) {
            if (outputs != null) {
                // Divide up output evenly.
                float totalWeight = 0;
                foreach (Synapse s in outputs) {
                    totalWeight += s.weight;
                }
                foreach (Synapse s in outputs) {
                    s.IndicateSpiked();
                    float deltaV = valueMax / totalWeight;
                    // Debug.Log("updating s.output.value: " + s.output.value + " with deltaV: " + deltaV);
                    s.output.value = Mathf.Max(s.output.value + deltaV, s.output.valueMax);
                }
            }

            value = valueMin;
        }

        // How far the node is rotated indicates how much v it has.
        // float r = value * 360.0f;
        // transform.eulerAngles = new Vector3(0, 0, r);
    }
}
