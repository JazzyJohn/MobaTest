using UnityEngine;
using System.Collections;

public class Building : DamageableObject
{
	protected Player owner= null;
	public void ActionClick(DamageableObject target, Vector3 point, Player comander){
		if(comander!=owner){
			return;
		}
		if(target!=null){
			Pawn targetPawn = target.GetComponent<Pawn>();
			if(targetPawn!=null&&targetPawn.team == team){
				FollowTarget(target);
			}else{
				AttackTarget(target);	
			}
		}
	}
}