using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour 
{
	private GameObject[] Tanks;
	public GameObject SpawnedTanks;
	public GameObject[] TankSpawners;
	public GameObject STankPref;
	public Transform TASPTransform;
	
	public static Transform CurrentTank;
	
	private TankController TC;
	
	private int index;
	private int TurnID = 0;
	private int SpawnCount = 1;
	
	public static int NumberOfPlayers = 10;
	public static int Player = 0;
	
	Vector3 SpawnPosition;
	
	
	
	UnityEngine.Random Rand;
	
	//bool[] PlayerTurn;
	
	// Use this for initialization
	void Start ()
	{	
		TankSpawners = GameObject.FindGameObjectsWithTag("Spawners");
		SpawnTanks();
		
		IComparer CompareObjects = new sortingClass();	
		Array.Sort(TankSpawners, CompareObjects);
		
		Tanks = GameObject.FindGameObjectsWithTag("Tank");
		//CurrentTracks = CurrentTank.
		
		Array.Sort(Tanks, CompareObjects);
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		CheckTurn();
//		Debug.Log("Current tank: " + CurrentTank.name);
	}
	
	void CheckTurn()
	{
		if(Player == NumberOfPlayers)
		{
			Player = 0;
		}
		CurrentTank = Tanks[Player].transform;
		TankController ControllIt  = gameObject.GetComponent<TankController>();
				
		//Debug.Log(CurrentTank.name);
	}
	
	void SpawnTanks()
	{	
	// List
		List<GameObject> TankSpawnPoints = new List<GameObject>(TankSpawners);

		for (int i = 0; i < NumberOfPlayers; i++)
		{
			int index = UnityEngine.Random.Range(0, TankSpawnPoints.Count);
			
				if (TankSpawnPoints.Count <=0)
				{
					return;
				}
			SpawnedTanks = Instantiate(STankPref,
			TankSpawnPoints[index].transform.position,
			Quaternion.identity) as GameObject;
			TankSpawnPoints.RemoveAt(index);
			SpawnedTanks.name = SpawnedTanks.name.Replace("(Clone)", "") + (i + 1);
			//TankController.Turret.rotation = Quaternion.Euler(0,TankController.Turret.rotation.y + 180,0);
		}
	}
}


// sorts the tanks by name	
public class sortingClass : IComparer  
{
    int IComparer.Compare( System.Object x, System.Object y )  
		{
     		 return((new CaseInsensitiveComparer()).Compare(((GameObject)x).name, ((GameObject)y).name));
		}
}
