using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public PhotonView photonView;
	
	public GameObject selectedClassPrefab;
	
	protected Pawn myPawn;
	
	protected int team;
	
	protected respawnDelay = 5.0f;
	
	protected respawnTimer = 0.0f;
	
	protected SelectableObject[] selectedObject;
	
	protected Vector3 mouseDownStart;
			
	protected Vector3 mouseUpPosition;	
	
	protected Camera mainCamera;
	
	public void SetTeam(int newTeam){
		team =newTeam
	}
	public int GetTeam(){
		return team;
	}
	
	void Start(){
		mainCamera=Camera.main;
	
	}
	
	void Update(){
		if(myPawn==null){
			myPawn =  PlayerManager.instance.SpawHero(selectedClassPrefab,team);
		}else{
			if(myPawn.IsDead()){
				respawnTimer-=Time.deltaTime;		
				if(respawnTimer<=0){
					myPawn.ReSpawn();
					PostSpawnAction();
				}
			}
		}
	
		if(Input.GetMouseButtonDown(0)){
			mouseDownStart = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0)){
			mouseUpPosition =Input.mousePosition;
			
			if((mouseUpPosition-mouseDownStart).sqrMagnitude >100.0f){
				SelectMultiply();
			
			}else{
				SelectSingle();
			}
		
		}
		if(Input.GetMouseButtonUp(1)){
			Ray ray = mainCamera.ScreenPointToRay(mouseUpPosition);
			RaycastHit[] hits =  Physics.RaycastAll(Ray);
			DamageableObject target= null;
			Vector3 clickTarget  =  Vector3.zero;
			foreach( RaycastHit hit in hits){
				if(hit.CompareTag("Damageable")){
					target  = hit.collider.GetComponent<DamageableObject>();
					break;
				}
				if(hit.CompareTag("Terrain")){
					clickTarget = hit.point;
				}
			}
			foreach(SelectableObject obj in selectedObject){
				
				obj.ActionClick(target,clickTarget,owner);
			}	
		
		}
	
	}
	
	void SelectSingle(){
		Ray ray = mainCamera.ScreenPointToRay(mouseUpPosition);
		selectedObject = new SelectableObject(1);
		RaycastHit[] hits =  Physics.RaycastAll(Ray);
		foreach( RaycastHit hit in hits){
			SelectableObject newSelected = hit.collider.GetComponent<SelectableObject>();
			if(newSelected!=null){
				selectedObject[0] = newSelected;
				return;
			}		
		}
		
		selectedObject[0] = myPawn.GetComponent<SelectableObject>(); 
	}
	void SelectMultiply(){
		List<Pawn> pawns = PlayerManager.instance.GetAllPawn();
		Rect rect = new Rect(mouseDownStart.x, mouseDownStart.y,mouseUpPosition.x , mouseUpPosition.y);
		List<SelectableObject> selected = new List<SelectableObject> ();
		foreach(Pawn pawn in pawns){
			Vector3  screenPos = mainCamera.WorldToScreenPoint(pawn.myTransform.postion);
			if(rect.Contains(screenPos,true)){
				if(pawn.owner ==this){
					selected.Add(pawn.GetComponent<SelectableObject>());				
				}
			}
			
		}
		SelectableObject[]  =selected.ToArray();
	}
	void PostSpawnAction(){
		myPawn.Init(this);
	
	}
}