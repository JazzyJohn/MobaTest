using UnityEngine;
using System.Collections;
public class DamageType{



}

public class DamageableObject : MonoBehaviour{
	
	public float health;
	
	public void Damage(DamageType type, float amount, GameObject killer){
		if(!NeedDamage(type,killer)){
			return;
		}
		amount = ModifyDamage(type,amount,killer);
		health-= amount;
		if(health<=0){
			KillMe();
		}
	
	}
	public virtual void KillMe(){
		Destroy(gameObject);
	}
	public virtual bool NeedDamage(DamageType type, GameObject killer){
		return true;
	}
	public virtual float ModifyDamage(DamageType type, float amount, GameObject killer){
		return amount;
	}
	



}