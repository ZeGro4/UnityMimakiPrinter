using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RozetkaHandler : MonoBehaviour {

	
	
	private Animator animator;
	
	bool isOn = false;
	
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetKeyDown(KeyCode.E) && !isOn) {


            animator.SetBool("On", !animator.GetBool("On"));
        }
        //if (Input.GetKeyDown(KeyCode.E) && isOn)
        //{

        //    animator.SetBool("On", false);
        //    isOn = false;
        //}
    }
}
