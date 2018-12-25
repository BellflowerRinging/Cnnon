using UnityEngine;
using System.Collections;
//This script executes commands to change character animations
[RequireComponent (typeof (Animator))]
public class Tower_Actions : MonoBehaviour {

	public Transform platform;
	public Animator animator { get; protected set; }
	private Quaternion rotate_State;
	private bool bool_fire;
 

	void Awake () {
		animator = GetComponent<Animator> ();
    }
 
	void LateUpdate(){
		if (bool_fire) {
			//platform.rotation=rotate_State;
		}
	}


	public void Idle()
	{
		animator.SetBool ("Idle", true);
		bool_fire = false;
	}
	public void Fire()
	{
 
		//rotate_State=platform.rotation;
		bool_fire = true;		
		animator.SetBool ("Fire", true);
 	}

    public void MenuIdel ()
    {
        bool_fire = false;

        animator.SetBool("MenuIdle", true);
    }


}
