using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

public class Network : MonoBehaviour
{
    public GameObject[,] neurons;

    // Start is called before the first frame update.
    void Start()
    {
        Bounds areaBounds = GetComponent<BoxCollider2D>().bounds;

        // float neuronRadius = (Resources.Load("Neuron") as GameObject).GetComponent<CircleCollider2D>().radius;
        // Debug.Log("Radius: " + neuronRadius);

        // Compute how many neurons will fit along each axis.
        // float dx = (neuronRadius * 4f);
        // float dy = (neuronRadius * 4f);
        // float numX = areaBounds.size.x / dx;
        // float numY = areaBounds.size.y / dy;

        // Instantiate all the neurons.
        // neurons = new GameObject[(int)numX, (int)numY];
        // // for (int i = 0; i < neurons.Length; i++) {
        // for (var y = 0; y < numY; y++)
        // {
        //     for (var x = 0; x < numX; x++)
        //     {
        //         // float x = UnityEngine.Random.Range(areaBounds.min.x, areaBounds.max.x);
        //         // float y = UnityEngine.Random.Range(areaBounds.min.y, areaBounds.max.y);
        //         neurons[x,y] = Instantiate(Resources.Load("Neuron")) as GameObject;
        //         neurons[x,y].transform.position = new Vector3(x, y, 0.0f);
        //     }
        // }

        // Put them in random locations.
         
        // }

        // neurons = new GameObject[4];
        // neurons[0] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[0].transform.position = new Vector3(-5f, -5f, 0.0f);
        
        // neurons[1] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[1].transform.position = new Vector3(-5f, 5f, 0.0f);
        
        // neurons[2] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[2].transform.position = new Vector3(5f, 5f, 0.0f);
        
        // neurons[3] = Instantiate(Resources.Load("Neuron")) as GameObject;
        // neurons[3].transform.position = new Vector3(5f, -5f, 0.0f);

        ConnectAllNeurons();
    }

    // public Neuron AddNeuron() {
    //     Bounds areaBounds = GetComponent<BoxCollider2D>().bounds;
    //
    //     float x = UnityEngine.Random.Range(areaBounds.min.x, areaBounds.max.x);
    //     float y = UnityEngine.Random.Range(areaBounds.min.y, areaBounds.max.y);
    //
    //     Array.Resize(ref neurons, neurons.Length + 1);
    //     neurons[neurons.Length - 1] = Instantiate(Resources.Load("Neuron")) as GameObject;
    //     neurons[neurons.Length - 1].transform.position = new Vector3(x, y, 0.0f);
    //
    //     Neuron n = neurons[neurons.Length - 1].GetComponent<Neuron>();
    //     
    //     ConnectNeuronToNeighbors(n);
    //     
    //     return n;
    // }

    private void ConnectNeurons(Neuron a, Neuron b)
    {
        // Before connecting any neurons, check if they are already connected.
        if (a == b)
        {
            Debug.Log("Cannot connect neuron to itself");
            return;
        }
        if (a == null || b == null)
        {
            Debug.Log("at least one neuron is null, not connecting");
            return; 
        }
        if (a.outputs.Any(s => s.output == b) || b.outputs.Any(s => s.output == a))
        {
            Debug.Log("neurons already connected, not connecting");
            return; 
        }

        // Create a synapse between the neurons...

        // First create a spring to hold the game objects together.
        SpringJoint2D spring = a.gameObject.AddComponent<SpringJoint2D>();
        // DistanceJoint2D spring = neurons[i].AddComponent<DistanceJoint2D>();
        // Debug.Log("spring distance: " + spring.distance);
        if (spring.distance < 5)
        {
            spring.distance = 5;
        }
        spring.connectedBody = b.gameObject.GetComponent<Rigidbody2D>();
        spring.enableCollision = true;

        // Then add the actual synapse.
        GameObject sGO = new GameObject();
        sGO = Instantiate(Resources.Load("Synapse")) as GameObject;
        Synapse synapse = sGO.GetComponent<Synapse>();
        synapse.weight = UnityEngine.Random.Range(0.01f, 1.0f);
        synapse.input = a;
        synapse.output = b;
        a.outputs.Add(synapse);
        Debug.Log("created new synapse, num outputs: " + a.outputs.Count);
    }

    private void ConnectNeuronToNeighbors(Neuron n)
    {
        for (int y = 0; y < neurons.GetLength(1); y++)
        {
            for (int x = 0; x < neurons.GetLength(0); x++)
            {
                Neuron b = neurons[x, y].GetComponent<Neuron>();
                Debug.Log("hi");
                if (n == b)
                {
                    continue;
                }

                Debug.Log("hi");

                float distance = Vector3.Distance(n.transform.position, b.transform.position);
                if (distance < 2)
                {
                    ConnectNeurons(n, b);
                }
            }
        }
    }

    public void ConnectAllNeurons(){
        if (neurons == null)
        {
            Debug.Log("Cannot connect neurons as they are null");
            return;
        }
        
        for (var ya = 0; ya < neurons.GetLength(1); ya++) {
            for (var xa = 0; xa < neurons.GetLength(0); xa++) {
                Neuron a = neurons[xa,ya].GetComponent<Neuron>();

                for (var yb = 0; yb < neurons.GetLength(1); yb++)
                {
                    for (var xb = 0; xb < neurons.GetLength(0); xb++)
                    {
                        Neuron b = neurons[xb,yb].GetComponent<Neuron>();
                        
                        var distance = Vector3.Distance(a.transform.position,b.transform.position);
                        if (distance < 3) {
                            // Create a synapse between the neurons.
                            ConnectNeurons(a, b);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (neurons == null)
        {
            Debug.Log("Network.Update(), but there are no neurons to update!");
            return;
        }
        // if (Input.GetKey(KeyCode.Escape))
        // {
        //     Application.Quit();
        //     Debug.Log("will you quit it please?");
        // }

        // Manually keep all neurons in bounds.
        Bounds areaBounds = GetComponent<BoxCollider2D>().bounds;
        for (int y = 0; y < neurons.GetLength(1); y++) {
            for (int x = 0; x < neurons.GetLength(0); x++)
            {
                GameObject n = neurons[x, y];
                float radius = n.GetComponent<CircleCollider2D>().radius;
                if (n.transform.position.x - radius < areaBounds.min.x)
                {
                    n.transform.position = new Vector3(areaBounds.min.x + radius, n.transform.position.y, 0.0f);
                }
                else if (n.transform.position.x + radius > areaBounds.max.x)
                {
                    n.transform.position = new Vector3(areaBounds.max.x - radius, n.transform.position.y, 0.0f);
                }
                else if (n.transform.position.y - radius < areaBounds.min.y)
                {
                    n.transform.position = new Vector3(n.transform.position.x, areaBounds.min.y + radius, 0.0f);
                }
                else if (n.transform.position.y + radius > areaBounds.max.y)
                {
                    n.transform.position = new Vector3(n.transform.position.x, areaBounds.max.y - radius, 0.0f);
                }
            }
        }

        // Draw a box around the network's outer bounds.
        LineRenderer line = GetComponent<LineRenderer>();

        List<Vector3> pos = new List<Vector3>();

        pos.Add(new Vector3(areaBounds.min.x, areaBounds.min.y));
        pos.Add(new Vector3(areaBounds.max.x, areaBounds.min.y));
        pos.Add(new Vector3(areaBounds.max.x, areaBounds.max.y));
        pos.Add(new Vector3(areaBounds.min.x, areaBounds.max.y));
        line.SetPositions(pos.ToArray());
        line.useWorldSpace = true;

        Color startColor = new Color(1, 1, 1, .1f);
        Color endColor = new Color(1, 1, 1, .1f);
        line.startColor = startColor;
        line.endColor = endColor;
    }

    // Creates an input layer with the given dimensions and returns
    // the neurons so the caller can use them to input to the network.
    public Neuron[,] CreateInputLayer(int width, int height)
    {
        Debug.Log("CreateInputLayer...");
        
        // Resize network to match aspect ratio of camera view.
        var neuronRadius = (Resources.Load("Neuron") as GameObject).GetComponent<CircleCollider2D>().radius;
        var scalar = neuronRadius * 2.5f;
        GetComponent<BoxCollider2D>().size = new Vector3(width * scalar, height * scalar, 0) ;
        
        // Create a single neuron for each pixel.
        neurons = new GameObject[(int)width, (int)height];
        
        // Collect non-game object type Neurons to return.
        var ns = new Neuron[(int)width, (int)height];
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var xLoc = x * scalar - GetComponent<BoxCollider2D>().size.x / 2f + transform.position.x;
                var yLoc = y * scalar - GetComponent<BoxCollider2D>().size.y / 2f + transform.position.y;
                neurons[x,y] = Instantiate(Resources.Load("Neuron")) as GameObject;
                neurons[x,y].transform.position = new Vector3(xLoc, yLoc, 0.0f);
                
                ns[x,y] = neurons[x,y].GetComponent<Neuron>();
            }
        }
        
        ConnectAllNeurons();
        
        // Return the list of neurons and we'll save it as input neurons.
        return ns;
    }
}
