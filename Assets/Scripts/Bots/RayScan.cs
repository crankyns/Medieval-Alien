using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class RayScan : MonoBehaviour {
	
	public string targetTag = "Player";
	public int rays = 12;
	public int distance = 7;
	public float angle = 90;

	MeshCreator meshCreator;
	
	public Vector3 offset;
	private Transform target;

	NavMeshAgent NMA;

	List<Vector3> vertices = new List<Vector3>();


	

	void Start () 
	{
		
		meshCreator = FindObjectOfType<MeshCreator>();
		NMA = GetComponent<NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag(targetTag).transform;
		offset = new Vector3(0, 1, 0);

	}

	bool GetRaycast(Vector3 dir)
	{
		bool result = false;
		RaycastHit hit = new RaycastHit();
		Vector3 pos = transform.position + offset;
		if (Physics.Raycast (pos, dir, out hit, distance))
		{
			if(hit.transform == target)
			{
				result = true;
				Debug.DrawLine(pos, hit.point, Color.green);
				vertices.Add(transform.InverseTransformPoint(hit.point));

			}
			else
			{
				Debug.DrawLine(pos, hit.point, Color.blue);
				vertices.Add(transform.InverseTransformPoint(hit.point));
			}
		}
		else
		{
			Debug.DrawRay(pos, dir * distance, Color.red);
			vertices.Add(transform.InverseTransformPoint(pos + dir * distance));
		}
		return result;
	}
	
	bool RayToScan () 
	{
		bool result = false;
		bool a = false;
		bool b = false;
		float j = 0;
		for (int i = 0; i < rays; i++)
		{
			var x = Mathf.Sin(j);
			var y = Mathf.Cos(j);

			j += angle * Mathf.Deg2Rad / rays;

			Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));
			if(GetRaycast(dir)) a = true;

			if(x != 0) 
			{
				dir = transform.TransformDirection(new Vector3(-x, 0, y));
				if(GetRaycast(dir)) b = true;
			}
		}
	
		if(a || b) result = true;
		return result;
	}

	void Update ()
	{	

		if(Vector3.Distance(transform.position, target.position) < distance)
		{
			meshCreator.vertices = vertices;
			vertices.Clear();
			

			if(RayToScan())
			{
				
				NMA.enabled = true;
				NMA.isStopped = false;
				GetComponent<BotController>().isFollowing = true;
				print(FindObjectOfType<BotController>().isFollowing);
				NMA.SetDestination(target.position);
				transform.GetChild(7).gameObject.SetActive(true);
			}
			else
			{


					NMA.isStopped = true;
					// FindObjectOfType<BotController>().isFollowing = false;
					print("Lose contact");
					transform.GetChild(7).gameObject.SetActive(false);

			}
		}
		else
		{
			// NMA.SetDestination(transform.position);
			NMA.isStopped = true;
			GetComponent<BotController>().isFollowing = false;
			transform.GetChild(7).gameObject.SetActive(false);
			
		}
	}


}
