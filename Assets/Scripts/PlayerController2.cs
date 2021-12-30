using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called right before processing physics.
    void FixedUpdate()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        body.AddForce(new Vector2(dx,dy) * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Neuron"))
        {
            Neuron n = collision.gameObject.GetComponent<Neuron>();
            n.value = n.valueMax;
            Debug.Log("Collided with Neuron!");
            // count++;
            // SetCountText();
        }
    }
}
