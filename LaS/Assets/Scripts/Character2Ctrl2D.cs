
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Character2Ctrl2D : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = false;         //角色朝向
    [HideInInspector]
    public bool jump = false;                //跳跃开关
    [HideInInspector]
    public bool skill_1 = false;             //技能1开关
    [HideInInspector]
    public bool blinkmarkon = false;         //传送点判定

    [HideInInspector]
    public bool energy = false;
    public UIBehaviour en;
    public float damage;


    public float moveForce;

    public float maxSpeed;
    public float jumpForce;

    private GameObject marks;
    public GameObject blinkmark;
    private GameObject selectedMark;

    private GameObject lights;


    [HideInInspector]
    public bool islighted = false;

    [HideInInspector]
    public int selectedNum;                //选择阴影编号

    // Use this for initialization
    void Start()
    {
        selectedMark = GameObject.Find("Mark1");
        marks = GameObject.Find("BlinkMarks");

        selectedNum = 1;
        lights = GameObject.Find("Light");
        

    }

    private void FixedUpdate()
    {
        
            
        


        if (skill_1)
        {
            Blink();
        }
        else
        {
            Move();
            if (Input.GetButtonDown("X2") && blinkmarkon)
            {
                skill_1 = true;
                blinkmark.GetComponent<SpriteRenderer>().enabled = true;
                blinkmark.transform.position = selectedMark.transform.position;
            }

        }

    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal2");

        if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            jump = false;
        }
        if (h > 0 && !facingRight)

            Flip();

        else if (h < 0 && facingRight)

            Flip();
    }
    void Blink()
    {
        en.GetComponent<Image>().fillAmount -= damage;
        if (Input.GetButtonDown("LB2"))
        {
            selectedNum--;
            if (selectedNum == 0)
                selectedNum = marks.transform.childCount;
            selectedMark = GameObject.Find("Mark" + selectedNum);
            blinkmark.transform.position = selectedMark.transform.position;
        }
        if (Input.GetButtonDown("RB2"))
        {
            selectedNum++;
            if (selectedNum == marks.transform.childCount + 1)
                selectedNum = 1;
            selectedMark = GameObject.Find("Mark" + selectedNum);
            blinkmark.transform.position = selectedMark.transform.position;
        }
        if (Input.GetButtonDown("X2"))
        {
            transform.position = selectedMark.transform.position;
        }
        if (Input.GetButtonDown("B2") || !blinkmarkon)
        {
            skill_1 = false;
            blinkmark.GetComponent<SpriteRenderer>().enabled = false;
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





