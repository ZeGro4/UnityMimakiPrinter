using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {


	public Text textInstruction;
	public GameObject panel;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeInstruction(int num)
	{
		panel.SetActive(true);
		switch (num) { 
		
			case 0:
				textInstruction.text = "Для подключение кабеля нажмите кнопку E";
				break;
			case 1:
                textInstruction.text = "Для включения принтера нажмите на включатель";
                break;
			case 11:
                textInstruction.text = "Для печати на рубашке нажмите кнопки R G Y в зависимости от цвета, которого вы хотите полуить";
                break;

        }	
	}
}
