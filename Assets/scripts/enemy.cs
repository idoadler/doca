using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {

    public int health;

    public int attack()
    {
        // if enemy attacking return attack power
        // else return 0
        return 0;
        // update hit matter
    }

    public void getHit(int damage)
    {
        health -= damage;
        // if health < 0 die
        // update health matter
    }
}
