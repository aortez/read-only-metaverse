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
