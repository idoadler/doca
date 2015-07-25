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

    private Vector3 targetDir;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        targetDir = transform.position;
        change_dir_tmp = change_dir;
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
        
        Vector3 currDir = transform.rotation.eulerAngles;
        targetDir = target.transform.position - transform.position;
        
        //    rb.AddTorque(add_torque_force * transform.right);
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
        /*
        //rb.AddTorque(1, 0, 1);
        //rb.AddRelativeTorque(0, 0, 1);
        

        Debug.Log(targetDir);
        //blue 0,0,1
        Debug.Log(transform.forward);
        Debug.Log(diff_angle(transform.forward));
        //red 1,0,0
        Debug.Log(transform.right);
        Debug.Log(diff_angle(transform.right));
        //green 0,1,0
        Debug.Log(transform.up);
        Debug.Log(diff_angle(transform.up));

        //green to blue
        //rb.AddRelativeTorque(1, 0, 0);
        //blue to red
        //rb.AddRelativeTorque(0, 1, 0);
        //red to green
        //rb.AddRelativeTorque(0, 0, 1);

        int closest_axis = 0;

        if ((diff_angle(transform.forward) < diff_angle(transform.right)) &&
            (diff_angle(transform.forward) < diff_angle(transform.up)))
        {
            Debug.Log("Blue");
            closest_axis = 1;
        }
        else if ((diff_angle(transform.right) < diff_angle(transform.forward)) &&
            (diff_angle(transform.right) < diff_angle(transform.up)))
        {
            Debug.Log("red");
            closest_axis = 2;
        }
        else
        {
            Debug.Log("green");
            closest_axis = 3;
        }
        */
    }

    float diff_angle(Vector3 vec)
    {
        float angle = Vector3.Angle(vec, targetDir);
        if (angle == 180)
        {
            return 0;
        }
        if (angle > 90)
        {
            angle = 180 - angle;
        }
        return angle;
    }

}
