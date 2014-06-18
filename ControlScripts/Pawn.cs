using UnityEngine;
using System.Collections;

public class Pawn : DamageableObject
{
	public bool IsDead;
	
	protected Player owner= null;
	
	protected Transform myTransform;
	
	protected int team;
	
	protected Controller controller;
		
	protected void Awake(){
		myTransform = transform;
		
	}
	public Player GetPlayer(){
		return owner;
	}
	public int GetTeam(){
		return team;
	}
	public void ReSpawn(){
		IsDead = true;	
		
	}
	public override void KillMe(){
		myTransform.position = Vector3(0,0,-100000);
	}
	public void Init(Player newOwner){
		owner= newOwner;
		team = newOwner.GetTeam();
	}
	public void ReSpawn(){
		myTransform.position = PlayerManager.instance.SpawHero(team);
	}
	public void ActionClick(DamageableObject target, Vector3 point, Player comander){
		if(comander!=owner){
			return;
		}
		if(target==null){
			controller.MoveTo(point);
		}else{
			Pawn targetPawn = target.GetComponent<Pawn>();
			if(targetPawn!=null&&targetPawn.team == team){
				controller.FollowTarget(target);
			}else{
				controller.AttackTarget(target);	
			}
		}
	}
	
	
	
}