using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageRegulator : MonoBehaviour {

	public CarriageScript carriage;

	enum State
	{
		defaultstate,
		State1,
		State2
	}
	State stateregulator;
	
	void Start () {
		stateregulator = State.defaultstate;
		RotateRegulator();

    }
	

	public void RotateHandler()
	{
		if (stateregulator == State.defaultstate) { 
			stateregulator = State.State1 ;
		}
		else if (stateregulator == State.State1) {
			stateregulator = State.State2 ;
		}

		else if (stateregulator == State.State2) {
			stateregulator = State.defaultstate;
		}
		RotateRegulator();
	}

	private void RotateRegulator()
	{
		switch (stateregulator) { 
		
			case State.defaultstate: 
				transform.rotation = Quaternion.Euler(-109.637f, -90, 180);
				carriage.DefaultCarriage();
                break;
			case State.State1:
                transform.rotation = Quaternion.Euler(-122.777f, -90, 180);
				carriage.UpCarriage();
                break;
            case State.State2:
                transform.rotation = Quaternion.Euler(-94.367f, -90, 180);
				carriage.DownCarriage();
                break;
        }
	}

}
