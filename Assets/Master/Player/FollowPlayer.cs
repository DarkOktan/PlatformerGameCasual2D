using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Transform Background_1;
    public Transform Background_2;
    public Transform Background_3;

    public float maxPos = 12f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x != transform.position.x && player.position.x > -9.8f && player.position.x < maxPos)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
        }

        Background_1.transform.position = new Vector2(transform.position.x * 1.0f, Background_1.transform.position.y);
        Background_2.transform.position = new Vector2(transform.position.x * 0.8f, Background_2.transform.position.y);
        Background_3.transform.position = new Vector2(transform.position.x * 0.6f, Background_3.transform.position.y);
    } 
}
