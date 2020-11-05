using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //Variables
    public Transform player;
    public float smooth = 0.3f;
    //Methods

    public float height = 7f;
    public float zOffset = 7f;

    private Vector3 velocity = Vector3.zero;

    private float nextTimeToSearch = 0;
   
    private void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.z = player.position.z - zOffset;
        pos.y = player.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);

    }
    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            if (searchResult != null)
            {
                player = searchResult.transform;
            }
            nextTimeToSearch = Time.time + 0.5f;
        }
    }
}
