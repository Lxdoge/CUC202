using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl2D : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = false;         //角色朝向
    [HideInInspector]
    public bool jump = false;				 //跳跃开关
    [HideInInspector]
    public bool skill_1 = false;             //技能1开关
    [HideInInspector]
    public bool skill_2 = false;             //技能2开关

    public float moveForce;
    public float maxSpeed;
    public float jumpForce;

    private GameObject lights;
    private GameObject selectedLight;
    
    [HideInInspector]
    public int selectedNum;                //选择光源编号
    

    // Use this for initialization
    void Start (){
        selectedLight = GameObject.Find("2DLight1");
        lights = GameObject.Find("Light");
        
        selectedNum = 1;

    }

    private void FixedUpdate(){
        if (skill_1)
        {
            TurnLight();
        }
        else
        {
            Move();
            if (Input.GetButtonDown("A1"))
                skill_1 = true;
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
                gameObject.layer = 11;
            }
        }
        
        
        
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

    void TurnLight()//转动光源!
    {

        if (Input.GetButtonDown("LB1"))
        {
            selectedNum--;
            if (selectedNum == 0)
                selectedNum = lights.transform.childCount;
        }
        if (Input.GetButtonDown("RB1"))
        {
            selectedNum++;
            if (selectedNum == lights.transform.childCount + 1)
                selectedNum = 1;
        }
        if(Input.GetButtonDown("A1"))
        {
            selectedLight = GameObject.Find("2DLight"+selectedNum);
        }
        selectedLight.transform.RotateAround(selectedLight.transform.GetChild(0).transform.position, Vector3.back, Input.GetAxis("Horizontal1"));
            
        //Debug.Log(selectedNum);
        if (Input.GetButtonDown("B1"))
            skill_1 = false;
    }

    

}
