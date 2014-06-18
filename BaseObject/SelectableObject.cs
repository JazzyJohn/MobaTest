using UnityEngine;
using System.Collections;

public class SelectableObject : MonoBehaviour{
	
	public Pawn curPawn;
	
	public Building curBuilding;
	
	public void ActionClick(DamageableObject target, Vector3 point, Player comander){
		if(curPawn !=null){
			curPawn.ActionClick(target, point, comander);
		
		}
		if(curBuilding !=null){
			curBuilding.ActionClick(target, point, comander);
		
		}
	}



}