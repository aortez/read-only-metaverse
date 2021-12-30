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
                    Debug.Log("updating s.other.value: " + s.other.value + " with deltaV: " + deltaV);
                    s.other.value += deltaV;
                }
            }

            value = valueMin;
        }
        // Debug.Log("updated self value: " + value);

        // How far the node is rotated indicates how much v it has.
        float r = value * 360.0f;
        transform.eulerAngles = new Vector3(0, 0, r);
    }
}
