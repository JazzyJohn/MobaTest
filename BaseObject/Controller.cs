using UnityEngine;
using System;
using System.Collections;
public enum PawnState {
	Stand,
	Move,
	Follow,
	Attack,

}
public class Controller: MonoBehaviour{
	public Transform movementTarget = null;
	
	public Vector3 position =null;
	
	public DamageableObject Enemy= null;

	protected PawnState  state;
	
	public Pawn pawn;
	
	public AIAgentComponent agent;
	
	void Awake(){
		agent = GetComponent<AIAgentComponent>();
	}
	
	void FixedUpdate(){
		switch(state){
			case PawnState.Stand:
			
			break;
			case Move:
				if(_MoveTo()){
					state=PawnState.Stand;
				}			
			break;
			case Follow:
				if(_Follow()){
					state=PawnState.Stand;
				}			
			break;
			case Attack:
				if(_Follow(pawn.MaxAttackDistance())){
					if(_AttackTarget()){
						state=PawnState.Stand;
					}
				}			
			break;
		}
	
	}
	public void MoveTo(Vector3  target){
		position= target;
		agent.SetTarget(target);
		state=PawnState.Move;
	}
	public void FollowTarget(DamageableObject  target){
		movementTarget= target.transform;
		state=PawnState.Follow;
	}
	public void AttackTarget(DamageableObject  target){
		movementTarget= target.transform;
		state=PawnState.Follow;
	}

	public void StandStill(){
		state=PawnState.Stand;
	}
	
	protected void _MoveTo(){
		agent.WalkUpdate ();		
	
	}
	protected void _Follow(){
		agent.SetTarget(movementTarget.position,true);
		agent.WalkUpdate ();		
	}
}