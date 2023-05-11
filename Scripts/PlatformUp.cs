using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUp : MonoBehaviour
{
    private bool move = false;
    public int speed = 7;
    public float PosY = -4.809999f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!move) return;
        var step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, PosY, transform.position.z),step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            move = true;
        }
    }
}
