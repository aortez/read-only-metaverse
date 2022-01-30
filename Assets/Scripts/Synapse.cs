using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : MonoBehaviour
{
    public float weight;

    public Neuron input;
    public Neuron output;

    private int recentSpikeCounter = 0;
    const int recentSpikeCounterMax = 240;

    public Synapse(float weight, Neuron input, Neuron output) {
        this.weight = weight;
        this.input = input;
        this.output = output;
    }

    public void IndicateSpiked() {
        recentSpikeCounter = recentSpikeCounterMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (output == null) {
            Debug.Log("+++++ created new Synapse, weight: " + weight + ", output: " + null);
        }
        else if (input == null) {
            Debug.Log("+++++ created new Synapse, weight: " + weight + ", input: " + null);
        } else {
            Debug.Log("+++++ created new Synapse, weight: " + weight + ", output: " + output);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (output == null || input == null) {
            Debug.Log("cannot draw synapse without both an input and output!!!");
            return;
        }

        LineRenderer line = GetComponent<LineRenderer>();

        List<Vector3> pos = new List<Vector3>();
        pos.Add(input.transform.position);
        pos.Add(output.transform.position);
        line.SetPositions(pos.ToArray());

        line.startWidth = 1.0f;
        line.endWidth = 0.1f;

        line.useWorldSpace = true;

        // Color startColor = new Color(1, 1, 1, .1f);
        // Color endColor = new Color(1, 1, 1, .1f);
        float alpha = recentSpikeCounter / (float)recentSpikeCounterMax + 0.1f;
        float beta = 1.0f - alpha + 0.05f;
        Color startColor = new Color(alpha, beta, beta, alpha + 0.1f);
        Color endColor = new Color(alpha, beta, beta, alpha + 0.1f);
        line.startColor = startColor;
        line.endColor = endColor;
        if (recentSpikeCounter > 0) {
            recentSpikeCounter--;
            // Debug.Log("recentSpikeCounter: " + recentSpikeCounter + ", alpha: " + alpha + ", beta: " + beta);
        }
    }
}
