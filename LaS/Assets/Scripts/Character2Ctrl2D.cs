using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Ctrl2D : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = false;         //角色朝向
    [HideInInspector]
    public bool jump = false;                //跳跃开关
    [HideInInspector]
    public bool skill_1 = false;             //技能1开关


    public float moveForce;
    public float maxSpeed;
    public float jumpForce;

    // Use this for initialization
    void Start () {
        
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal1");

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
    }
    void Blink()
    {

    }
}
