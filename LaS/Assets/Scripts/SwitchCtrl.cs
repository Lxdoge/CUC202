using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCtrl : MonoBehaviour {
    public GameObject pillars_m;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "player2")
        {
            if (Input.GetButtonDown("A2"))
            {
                if (!GetComponent<Animator>().GetBool("switchOn"))
                {
                    GetComponent<Animator>().SetBool("switchOn", true);
                    pillars_m.GetComponent<Animator>().SetBool("pillars_mOn", true);
                    
                }
                else
                {
                    GetComponent<Animator>().SetBool("switchOn", false);
                    pillars_m.GetComponent<Animator>().SetBool("pillars_mOn", false);
                }

            }
        }
    }

}
