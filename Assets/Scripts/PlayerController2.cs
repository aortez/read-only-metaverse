using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Debug.Log("PlayerController2 FixedUpdate()");
        // var keyboard = Keyboard.current;
        // if (keyboard == null) {
        //      Debug.Log("no keyboard detected");
        //      return;
        // } else {
        //     Debug.Log("yes keyboard detected");
        //     if(keyboard.anyKey.wasPressedThisFrame)
        //     {
        //            Debug.Log("A key was pressed");
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     Debug.Log("Space was pressed!");
        // }
        //
        // float dx = Input.GetAxis("Horizontal");
        // float dy = Input.GetAxis("Vertical");
        // body.AddForce(new Vector2(dx,dy) * Time.deltaTime * 1000);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neuron"))
        {
            Neuron n = collision.gameObject.GetComponent<Neuron>();
            n.value = Mathf.Max(n.value + n.valueMax * 0.01f, n.valueMax);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neuron"))
        {
            Neuron n = collision.gameObject.GetComponent<Neuron>();
            n.value = Mathf.Max(n.value + n.valueMax * 0.25f, n.valueMax);
        }
    }
}
