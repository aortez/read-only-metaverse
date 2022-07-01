using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody2D body;

    // The space ship sees the world through this texture.
    public RenderTexture view;

    public Network network;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        // fireAction = new InputAction(binding: "<Keyboard>/keyW");
        // fireAction.Enable();

        // fireAction.started += context => Debug.Log($"{context.action} started");
        // fireAction.performed += context => Debug.Log($"{context.action} performed");
        // fireAction.canceled += context => Debug.Log($"{context.action} canceled");
    }

    void FixedUpdate()
    {
        float rotationForce = 4;
        float forwardThrust = 1000;
        if (Keyboard.current.wKey.isPressed) {
            body.AddForce(body.transform.up * Time.deltaTime * forwardThrust);
        }
        if (Keyboard.current.sKey.isPressed) {
            body.AddForce(-body.transform.up * Time.deltaTime * forwardThrust);
        }
        if (Keyboard.current.aKey.isPressed) {
            body.AddTorque(Time.deltaTime * rotationForce, ForceMode2D.Impulse);
        }
        if (Keyboard.current.dKey.isPressed) {
            body.AddTorque(-Time.deltaTime * rotationForce, ForceMode2D.Impulse);
        }

        // Need to connect each input pixel to a neuron in the network.
        // network.
    }

    public Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D dest = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
        dest.Apply(false);
        Graphics.CopyTexture(rTex, dest);
        return dest;
    }

    // Update is called once per frame
    void Update()
    {
        Texture2D view2d = toTexture2D(view);

        // Grab pixels and lets look at em.
        Color [] pixels = view2d.GetPixels(0);
        
        Debug.Log("num pixels: " + pixels.Length);
        float average = 0;
        for (int y = 0; y < view2d.height; y++) {
            for (int x = 0; x < view2d.width; x++) {
                Color c = pixels[y * view2d.width + x];
                average += c.g;
            }
        }
        Debug.Log("average grayscale: " + average);


        LineRenderer line = GetComponent<LineRenderer>();
        // Set some positions
        // Vector3[] positions = new Vector3[16];
        // float angle = 0.0f;
        // float radius = GetComponent<Collider>().radius;
        // // Reserve one for the extra line.
        // float delta_angle = (2 * Mathf.PI) / (positions.Length - 2);
        // for (int i = 0; i < positions.Length - 1; i++) {
        //     positions[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0.0f);
        //     angle += delta_angle;
        // }
        // positions[positions.Length - 1] = new Vector3(0, 0, 0.0f);
        // line.positionCount = positions.Length;
        // line.loop = true;
        //
        // float spikeRatio = value / valueMax;
        // float red = 0.1f + spikeRatio;
        // float blue = 0.9f - red;
        // line.startColor = new Color(red, 0, blue, 1.0f);
        // line.endColor = new Color(red, 0, blue, 1.0f);
        // line.startWidth = 0.2f;
        // line.endWidth = 0.2f;
        // line.startWidth = Mathf.Min(spikeRatio, 1);
        // line.endWidth = Mathf.Min(spikeRatio, 1);
        // line.SetPositions(positions);
    }
}
