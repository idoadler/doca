using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

    public int health;
    public GameObject player;
    public float attacking_dist = 6.0f;
    public float stamina = 8.0f;
    private float stamina_tmp = 8.0f;
    public float jump_up_force = 300.0f;
    public float jump_to_player = 50.0f;

    private Rigidbody rb;
    private bool in_air = false;
    private find_top_edge find_top;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        find_top = GetComponent<find_top_edge>();
    }

    void Update()
    {
        stamina_tmp -= Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) < attacking_dist)
        {
            if (stamina_tmp < 0)
            {
                stamina_tmp = stamina;
                attack();
            }
        }
    }

    void attack()
    {
        rb.AddForce((player.transform.position - transform.position) * jump_to_player);
        rb.AddForce(0,jump_up_force,0);
        in_air = true;
    }


    public int get_damage()
    {
        if (!in_air)
        {
            return 0;
        }
        else
        {
            return find_top.get_top_edge_result();
        }
        // update hit matter
    }

    public void getHit(int damage)
    {
        health -= damage;
        // if health < 0 die
        if (health < 0)
        {
            Destroy(this.gameObject);
        }
        // update health matter
    }


    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (collision.gameObject.name == "Terrain")
            {
                in_air = false;
            }
        }
    }


}
