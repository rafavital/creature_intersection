using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] [Range (0, 10)] private float speed;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent <Rigidbody> ();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (horizontal, 0, vertical);

        rb.position -= movement.normalized * speed * Time.deltaTime;
    }
}
