using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl2D : MonoBehaviour {

    [HideInInspector]
    public float hor;                           //水平输入
    [HideInInspector]
    public bool facingRight = false;            //角色朝向
    [HideInInspector]
    public Rigidbody2D rb;                      //刚体
    [HideInInspector]
    public bool grounded = false;               //落地检验
    [HideInInspector]
    public bool jump = false;                   //跳跃开关
    [HideInInspector]
    public int jumps = 0;                       //跳跃计数
    [HideInInspector]
    public bool runningOnJump;					//跳跃时的移动状态
    [HideInInspector]
    public bool keepVelocityOnGround = false;   //惯性滑动/传送带移动开关
    [HideInInspector]
    public bool skill_1 = false;                //技能1开关
    [HideInInspector]
    public bool skill_2 = false;                //技能2开关

    //水平移动相关变量
    public float walkSpeed = 3f;                //行走速度
    public float runSpeed = 7f;                 //奔跑速度
    public float moveForce = 50f;               //移动刚体力

    //跳跃相关变量
    public float jumpTime = 0.3f;               //跳跃判断时间
    public int totalJumps = 2;                  //最大跳跃段数

    public float initialJump = 5f;              //跳跃初速度
    public float jumpForce = 20f;               //最大跳跃刚体力

    public float initialDoubleJump = 5f;        //二段跳初速度
    public float doubleJumpForce = 15f;         //最大二段跳刚体力

    private bool jumpInitial = false;           //跳跃启动检验
    private float jumpTimer;                    //跳跃判断剩余时间
    private Transform groundCheck;              //落地检验物体
    

    private GameObject lights;
    public GameObject lightmark;
    private GameObject selectedLight;
    
    [HideInInspector]
    public int selectedNum;                //选择光源编号
    

    // Use this for initialization
    void Start ()
    {
        selectedLight = GameObject.Find("2DLight1");
        lights = GameObject.Find("Light");
        
        selectedNum = 1;
        //实例化刚体组件和落地检验物体
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");

    }

    private void Update()
    {
        //获取水平输入
        hor = Input.GetAxis("Horizontal1");
        GroundCheck();
        JumpCheck();
    }

    private void FixedUpdate()
    {
        if (skill_1)
        {
            TurnLight();
        }
        else
        {
            Run();
            Jump();
            if (Input.GetButtonDown("Y1"))
            {
                skill_1 = true;
                lightmark.GetComponent<SpriteRenderer>().enabled = true;
                lightmark.transform.position = selectedLight.transform.position;
            }
                
        }
        if (skill_2)
        {
            if (Input.GetButtonDown("X1"))
            {
                skill_2 = false;
                gameObject.layer = 10;
            }
        }
        else
        {
            if (Input.GetButtonDown("X1"))
            {
                skill_2 = true;
                gameObject.layer = 12;
            }
        }
        
        
        
    }

    //获取当前状态的速度限制值
    public float GetSpeed()
    {
        float speed;
        //根据移动状态返回对应的速度
        if (grounded)
        {
            speed = isRun() ? runSpeed : walkSpeed;
        }
        //在空中
        else
        {
            speed = runningOnJump ? runSpeed : walkSpeed;
        }


        return speed;
    }
    //奔跑状态检验
    public bool isRun()
    {
        return Input.GetButton("RT1");
    }
    //水平移动
    public void Run()
    {
        float speed = GetSpeed();
        //如果速度小于限制值
        if (hor * rb.velocity.x < speed)
        {
            //加速（对刚体施力）
            rb.AddForce(Vector2.right * hor * moveForce);
        }
        //如果速度大于限制值
        if (hor * rb.velocity.x > speed)
        {
            //限制水平方向的速度
            rb.velocity = new Vector2(hor * speed, rb.velocity.y);
        }

        //根据移动方向翻转角色朝向
        if (hor > 0 && !facingRight)
            Flip();
        else if (hor < 0 && facingRight)
            Flip();
    }
    //检查并初始化跳跃
    public void JumpCheck()
    {
        if (!jump && grounded)
        {
            jumps = 0;
        }
        if (!jump && Input.GetButtonDown("A1") && jumps < totalJumps)
        {
            jump = true;
            jumpTimer = jumpTime;
            jumpInitial = true;
            runningOnJump = isRun();
        }
    }
    //跳跃
    public void Jump()
    {
        if (jump)
        {
            //如果跳跃刚刚启动
            if (jumpInitial)
            {
                //初次启动

                if (jumps == 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, initialJump);
                }
                //多段跳启动
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, initialDoubleJump);
                }
                jumpInitial = false;
                jumps++;
            }
            //在跳跃判定时间内，按住跳跃键会持续施加刚体力
            else if (Input.GetButton("A1") && jumpTimer > 0)
            {
                jumpTimer -= Time.fixedDeltaTime;
                if (jumps == 1)
                    rb.AddForce(Vector2.up * jumpForce);
                else
                    rb.AddForce(Vector2.up * doubleJumpForce);
            }
            else
            {
                jump = false;
            }
        }
    }
    //检查落地
    public void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << 13) || Physics2D.Linecast(transform.position, groundCheck.position, 1 << 14);
    }

    void TurnLight()//转动光源!
    {

        if (Input.GetButtonDown("LB1"))
        {
            selectedNum--;
            if (selectedNum == 0)
                selectedNum = lights.transform.childCount;
            selectedLight = GameObject.Find("2DLight" + selectedNum);
            lightmark.transform.position = selectedLight.transform.position;
        }
        if (Input.GetButtonDown("RB1"))
        {
            selectedNum++;
            if (selectedNum == lights.transform.childCount + 1)
                selectedNum = 1;
            selectedLight = GameObject.Find("2DLight" + selectedNum);
            lightmark.transform.position = selectedLight.transform.position;
        }
        selectedLight.transform.RotateAround(selectedLight.transform.GetChild(0).transform.position, Vector3.back, Input.GetAxis("Horizontal1"));
            
        //Debug.Log(selectedNum);
        if (Input.GetButtonDown("B1"))
        {
            skill_1 = false;
            lightmark.GetComponent<SpriteRenderer>().enabled = false;
        }
            
    }
    void Flip()
    {
        
        facingRight = !facingRight;

        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
