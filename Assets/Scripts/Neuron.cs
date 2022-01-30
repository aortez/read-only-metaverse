using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    const float valueMin = 0.0f;
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
        // Primitive spike and pass it on mechanism.
        value += growth;
        if (value > valueMax) {
            if (outputs != null) {
                float totalWeight = 0;
                foreach (Synapse s in outputs) {
                    totalWeight += s.weight;
                }
                // Debug.Log("total weight: " + totalWeight);
                foreach (Synapse s in outputs) {
                    s.IndicateSpiked();
                    float deltaV = valueMax / totalWeight;
                    // Debug.Log("updating s.output.value: " + s.output.value + " with deltaV: " + deltaV);
                    s.output.value += deltaV;
                }
            }

            value = valueMin;
        }

        // How far the node is rotated indicates how much v it has.
        float r = value * 360.0f;
        transform.eulerAngles = new Vector3(0, 0, r);

        LineRenderer line = GetComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();
        pos.Add(new Vector3(transform.position.x, transform.position.y, 1));
        pos.Add(new Vector3(transform.position.x, transform.position.y + value * 5, 1));
        line.SetPositions(pos.ToArray());
        line.startColor = Color.green;
        line.endColor = Color.green;
        line.startWidth = 1.0f;
        line.endWidth = 1.0f;
        line.useWorldSpace = true;
    }
}
