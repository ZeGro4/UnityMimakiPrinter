using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour {

	Animator animator;
	
	public AudioSource audioSource;
	public AudioClip audioClip;

	void Start () {
		animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown(KeyCode.L)) { 
			
				animator.SetBool("click",!animator.GetBool("click"));
				if(animator.GetBool("click")) audioSource.PlayOneShot(audioClip);
            else audioSource.Stop();
        }

	}
}
