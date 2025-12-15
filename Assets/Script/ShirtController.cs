using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtController : MonoBehaviour {

	Animator animator;
	public Material materialRed;
	public Material materialGreen;
	public Material materialYellow;

	public CarriageScript _carriageScript;

	public GameObject paper;

	bool isColored= false;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (animator != null)
		{
           
            if (Input.GetKeyDown(KeyCode.R))
			{
				ColoredShirt("red");
              

            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                ColoredShirt("green");
               

            }
			if (Input.GetKeyDown(KeyCode.Y))
			{
                ColoredShirt("yellow");
               
            }
			
        }

	}

	public void ColoredShirt(string color)
	{
		paper.SetActive(false);
        _carriageScript.StartMoving(9f);
        if (isColored) {
			return;
        }
        animator.SetBool("IsMoving", true);

        switch (color) {

			case "red":
                StartCoroutine(ColorShirt(materialRed));
				break;
			case "green":
				StartCoroutine(ColorShirt(materialGreen));
				break;
			case "yellow":
				StartCoroutine(ColorShirt(materialYellow));
				break;
        }
		isColored = true;
	}

	private IEnumerator ColorShirt(Material material)
	{
		yield return new WaitForSeconds(6f);
		gameObject.GetComponent<MeshRenderer>().material = material;
		paper.SetActive(true);
	}
}
