using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public GameObject doorNorth;
	public GameObject doorSouth;
	public GameObject doorEast;
	public GameObject doorWest;
    public GameObject rp;
    public GameObject shp;
    public GameObject sp;
    public GameObject spot1;
    public GameObject spot2;

    public int roomNum;

    public Transform[] spawnPoints = new Transform[2];

}