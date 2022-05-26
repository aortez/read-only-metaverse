using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShip : MonoBehaviour
{
    public float speed;

    public InputAction fireAction;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        fireAction = new InputAction(binding: "<Keyboard>/keyW");
        fireAction.Enable();

        fireAction.started += context => Debug.Log($"{context.action} started");
        fireAction.performed += context => Debug.Log($"{context.action} performed");
        fireAction.canceled += context => Debug.Log($"{context.action} canceled");

        // Base the PolygonCollider2D off of the LineRenderer path?
        // Might be able to find this code with little searching.
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
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

    // Update is called once per frame
    void Update()
    {
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
