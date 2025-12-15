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
            case 3:
                textInstruction.text = "Для установки высоты нажмите на рычаг, 1 - нажатие выше, 2 - ниже \n 3 - начальное положение";
                break;
            case 4:
                textInstruction.text = "Что бы закрепить руллонный носитель нажмите на кнопку P";
                break;
            case 5:
                textInstruction.text = "Что бы установить листовой носитель нажмите на кнопку B";
                break;

            case 6:
                textInstruction.text = "";
                break;

            case 8:
                textInstruction.text = "Для печати бумаги нажмите Z \nДля печати на рубашке нажмите кнопки R G Y в зависимости от цвета, которого вы хотите полуить";
                break;
			case 9:
                textInstruction.text = "Для печати бумаги нажмите Z \nДля печати на рубашке нажмите кнопки R G Y в зависимости от цвета, которого вы хотите полуить";
				break;
            

        }	
	}
}
