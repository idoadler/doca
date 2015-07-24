using UnityEngine;
using System.Collections;

public class cube_move : MonoBehaviour {

    public float try_to_move = 1.0f;
    private float try_to_move_tmp = 1.0f;

    public float change_dir = 1.0f;
    private float change_dir_tmp = 1.0f;
    
    public float add_torque_force = 1.0f;
    public float add_force = 1.0f;
    public float move_time = 1.0f;

    public GameObject target;
    public bool follow_cross = false;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        try_to_move_tmp -= Time.deltaTime;
        change_dir_tmp -= Time.deltaTime;

        if (change_dir_tmp < 0)
        {
            change_dir_func();
            change_dir_tmp = change_dir;
        }

    }

    void FixedUpdate()
    {

        if (try_to_move_tmp < 0)
        {
            if (try_to_move_tmp < -move_time)
            {
                try_to_move_tmp = try_to_move;
            }
            move();
        }

    }

    void change_dir_func()
    {
        Vector3 currDir = transform.rotation.eulerAngles;
        Vector3 targetDir = target.transform.position - transform.position;
        targetDir = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        //transform.rotation = Quaternion.LookRotation(targetDir);
    }


    void move()
    {
        //    rb.AddTorque(add_torque_force * transform.right);
        Vector3 currDir = transform.rotation.eulerAngles;
        Vector3 targetDir = target.transform.position - transform.position;
        //targetDir = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        targetDir = targetDir.normalized;
        targetDir.y = 0;
        targetDir *= add_torque_force;

        if (follow_cross)
        {
            rb.AddTorque(targetDir.z, 0, -targetDir.x);
        }
        else
        {
            if (Mathf.Abs(targetDir.x) > Mathf.Abs(targetDir.z))
            {
                if (targetDir.x > 0)
                {
                    rb.AddTorque(0, 0, -add_torque_force);
                }
                else
                {
                    rb.AddTorque(0, 0, add_torque_force);
                }
            }
            else
            {
                if (targetDir.z > 0)
                {
                    rb.AddTorque(add_torque_force, 0, 0);
                }
                else
                {
                    rb.AddTorque(-add_torque_force, 0, 0);
                }
            }
        }
        //rb.AddTorque(1, 0, 1);
        //rb.AddRelativeTorque(0, 0, 1);
    }

}
