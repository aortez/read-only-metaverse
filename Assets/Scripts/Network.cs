using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour
{
    public GameObject[] neurons;

    // Start is called before the first frame update.
    void Start()
    {
        Bounds areaBounds = GetComponent<BoxCollider2D>().bounds;

        float neuronRadius = (Resources.Load("Neuron") as GameObject).GetComponent<CircleCollider2D>().radius;
        Debug.Log("Radius: " + neuronRadius);

        // Compute how many neurons will fit along each axis.
        float dx = (neuronRadius * 4f);
        float dy = (neuronRadius * 4f);
        float numX = areaBounds.size.x / dx;
        float numY = areaBounds.size.y / dy;

        neurons = new GameObject[(int)numX * (int)numY];

        // Instantiate all the neurons.
        for (int i = 0; i < neurons.Length; i++) {
            // Put them in random locations.
            float x = Random.Range(areaBounds.min.x, areaBounds.max.x);
            float y = Random.Range(areaBounds.min.y, areaBounds.max.y);
            neurons[i] = Instantiate(Resources.Load("Neuron")) as GameObject;
            neurons[i].transform.position = new Vector3(x, y, 0.0f);
        }

        // neurons = new GameObject[4];
        // neurons[0] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[0].transform.position = new Vector3(-5f, -5f, 0.0f);
        //
        // neurons[1] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[1].transform.position = new Vector3(-5f, 5f, 0.0f);
        //
        // neurons[2] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[2].transform.position = new Vector3(5f, 5f, 0.0f);
        //
        // neurons[3] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[3].transform.position = new Vector3(5f, -5f, 0.0f);

        // Walk neurons and attach to other's within a given distance.
        for (int i = 0; i < neurons.Length - 1; i++) {
            Neuron a = neurons[i].GetComponent<Neuron>();
            for (int j = i + 1; j < neurons.Length; j++) {
                float distance = Vector3.Distance(neurons[i].transform.position, neurons[j].transform.position);

                if (distance < 8) {
                    Debug.Log("++++++ i, j, distance: " + i + ", " + j + ": " + distance);
                    Debug.Log("num outputs: " + a.outputs.Count);

                    // Create a synapse between the neurons...
                    Neuron b = neurons[j].GetComponent<Neuron>();

                    // First create a spring to hold the game objects together.
                    SpringJoint2D spring = neurons[i].AddComponent<SpringJoint2D>();
                    // DistanceJoint2D spring = neurons[i].AddComponent<DistanceJoint2D>();
                    Debug.Log("spring distance: " + spring.distance);
                    // if (spring.distance < 5) {
                        spring.distance = 8;
                    // }
                    spring.connectedBody = neurons[j].GetComponent<Rigidbody2D>();
                    spring.enableCollision = true;

                    // Then add the actual synapse.
                    GameObject s = new GameObject();
                    s = Instantiate(Resources.Load("Synapse")) as GameObject;
                    Synapse synapse = s.GetComponent<Synapse>();
                    synapse.weight = Random.Range(0.01f, 1.0f);
                    synapse.input = a;
                    synapse.output = b;
                    a.outputs.Add(synapse);
                } else {
                    Debug.Log("------ i, j, distance: " + i + ", " + j + ": " + distance);
                }
            }
            Debug.Log("num outputs: " + a.outputs.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("will you quit it please?");
        }

        // Don't do this, but this shows that we could do some higher end update here.
        // It would have to the kind where coordination between all the network
        // is needed.
        // for (int i = 0; i < neurons.Length; i++) {
        //     neurons[i].transform.Rotate(0, 0.0f, 1.0f);
        // }
    }
}
