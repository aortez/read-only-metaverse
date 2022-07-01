using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShip : MonoBehaviour
{
    private Rigidbody2D body;

    // The space ship sees the world through this texture.
    public RenderTexture view;

    public Network network;

    private bool isNetworkConnected = false;

    private Neuron n;

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
    }

    public Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D view2d = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);

        RenderTexture.active = rTex;
        view2d.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        view2d.Apply();
        return view2d;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNetworkConnected) {
            n = network.AddNeuron();
            isNetworkConnected = true;
        }

        // Grab pixels and lets look at em.
        Texture2D view2d = toTexture2D(view);
        Color [] pixels = view2d.GetPixels(0);

        // Debug.Log("num pixels: " + pixels.Length);
        float average = 0;
        for (int y = 0; y < view2d.height; y++) {
            for (int x = 0; x < view2d.width; x++) {
                Color c = pixels[y * view2d.width + x];
                average += c.g;
            }
        }
        average /= pixels.Length;
        average *= 0.01f;
        if (n != null) { n.AddPotential(average); }
        Debug.Log("average green: " + average);

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
