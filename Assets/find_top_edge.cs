using UnityEngine;
using System.Collections;
using UnityEditor;

public class find_top_edge : MonoBehaviour
{
    
    public int index = 0;
    public int score = 0;

    [System.Serializable]
    public class scores
    {
        public Vector3 vec;
        public int score;
    }

    public scores[] bule_axis;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {

    }
    /*
    blue 0,0,1
    Debug.Log(transform.forward);
    Debug.Log(diff_angle(transform.forward));
    //red 1,0,0
    Debug.Log(transform.right);
    Debug.Log(diff_angle(transform.right));
    //green 0,1,0
    Debug.Log(transform.up);
    Debug.Log(diff_angle(transform.up));
    */
    public void save_rotation()
    {
        bule_axis[index].vec = ido_transformtaion(transform.up, transform.right, transform.forward).normalized;
        Debug.Log(bule_axis[index].vec);
        bule_axis[index].score = score;
        score++;
        index++;
    }

    private Vector3 ido_transformtaion(Vector3 green, Vector3 red, Vector3 blue)
    {
        return new Vector3(green.y, red.y, blue.y);
    }

    public int get_top_edge_result()
    {
        int i;
        float min_dis = float.PositiveInfinity;
        float dis;
        int min_index = -1;
        for (i = 0; i < bule_axis.Length; i++)
        {
            dis = (bule_axis[i].vec - ido_transformtaion(transform.up, transform.right, transform.forward).normalized).magnitude;
            if (dis < min_dis)
            {
                min_dis = dis;
                min_index = i;
            }
        }

        return bule_axis[min_index].score;
    }
    
       
}