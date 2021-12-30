using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    float valueMin = 0.0f;
    float growth = 0.0001f;
    float valueMax = 1.0f;
    float value = 0.0f;

    public List<Synapse> outputs = new List<Synapse>();

    // Start is called before the first frame update.
    void Start()
    {
        value = Random.Range(valueMin, valueMax);
    }

    // Update is called once per frame.
    void Update()
    {
        // Primitive spike and pass it on mechanism.
        value += growth;
        if (value > valueMax) {
            if (outputs != null) {
                foreach (Synapse s in outputs) {
                    float deltaV = s.strength * valueMax;
                    Debug.Log("updating s.output.value: " + s.output.value + " with deltaV: " + deltaV);
                    s.output.value += deltaV;
                }
            }

            value = valueMin;
        }
        // Debug.Log("updated self value: " + value);

        // How far the node is rotated indicates how much v it has.
        float r = value * 360.0f;
        transform.eulerAngles = new Vector3(0, 0, r);

        LineRenderer line = GetComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();
        pos.Add(new Vector3(transform.position.x, transform.position.y, 1));
        pos.Add(new Vector3(transform.position.x, transform.position.y + value * 5, 1));
        // pos.Add(input.transform.position);
        // pos.Add(input.transform.position);
        // pos.Add(output.transform.position);
        line.SetPositions(pos.ToArray());
        line.startColor = Color.green;
        line.endColor = Color.green;
        Debug.Log("line.startColor: " + line.startColor);
        line.startWidth = 1.0f;
        line.endWidth = 1.0f;
        line.useWorldSpace = true;
    }
}
