using UnityEngine;
using System.Collections;

public class cube_move : MonoBehaviour {

    public float try_to_move = 1.0f;
    private float try_to_move_tmp = 1.0f;
    public float add_force = 1.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        try_to_move_tmp -= Time.deltaTime;
        if (try_to_move_tmp < 0)
        {
            try_to_move_tmp = try_to_move;
            move();
        }

    }

    void move()
    {
        rb.AddForce(add_force * Vector3.right );
    }

}
