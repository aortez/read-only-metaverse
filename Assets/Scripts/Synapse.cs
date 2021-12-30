using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : MonoBehaviour
{
    public float strength;

    public Neuron input;
    public Neuron output;

    public Synapse(float strength, Neuron input, Neuron output) {
        this.strength = strength;
        this.input = input;
        this.output = output;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (output == null) {
            Debug.Log("+++++ created new Synapse, strength: " + strength + ", output: " + null);
        }
        else if (input == null) {
            Debug.Log("+++++ created new Synapse, strength: " + strength + ", input: " + null);
        } else {
            Debug.Log("+++++ created new Synapse, strength: " + strength + ", output: " + output);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (output == null || input == null) {
            Debug.Log("not drawing synapse!!!");
            return;
        }

        LineRenderer line = GetComponent<LineRenderer>();
        List<Vector3> pos = new List<Vector3>();
        // pos.Add(new Vector3(0, 0, 1));
        pos.Add(input.transform.position);
        pos.Add(output.transform.position);
        line.SetPositions(pos.ToArray());
        line.startWidth = 1.0f;
        line.endWidth = 0.1f;
        line.useWorldSpace = true;
    }
}
