using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synapse : MonoBehaviour
{
    public float strength;

    public Neuron other;

    public Synapse(float strength, Neuron other) {
        this.strength = strength;
        this.other = other;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (other == null) {
            Debug.Log("+++++ created new Synapse, strength: " + strength + ", other: " + null);
        }
        Debug.Log("+++++ created new Synapse, strength: " + strength + ", other: " + other);
    }

    // Update is called once per frame
    void Update()
    {
        if (other != null) {
            LineRenderer line = GetComponent<LineRenderer>();
            Debug.Log("!!!!!!!!!!!drawing synapse!!!!!!!!!!!!");
            // // LineRenderer l = neurons[0].AddComponent<LineRenderer>();
            //
            List<Vector3> pos = new List<Vector3>();
            pos.Add(new Vector3(0, 0, 1));
            pos.Add(other.transform.position);
            // pos.Add(new Vector3(10, 10, 1));
            line.startWidth = 1f;
            line.endWidth = 1f;
            line.SetPositions(pos.ToArray());
            line.useWorldSpace = true;
        } else {
            // Debug.Log("..........");
        }
    }
}
