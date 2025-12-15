using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGenerator : MonoBehaviour {


	[SerializeField] GameObject papper;
	[SerializeField] CarriageScript carriage;
	
	void Update () {

		if (Input.GetKeyDown(KeyCode.Z)) { 
			StartPrint();
		}
	}


	public void StartPrint()
	{
		GameObject paper = Instantiate(papper, transform.position,Quaternion.identity);
		paper.GetComponent<PaperScript>().Printing();
		carriage.StartMoving(13f);


	}

	private IEnumerator KillPAper() { 
	
		yield return new WaitForSeconds(20);
		
	}
}
