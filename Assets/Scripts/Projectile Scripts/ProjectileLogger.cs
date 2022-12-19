using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProjectileLogger : MonoBehaviour
{
    private PathGenerator pg;
    public StreamWriter writetext;
    public int setNumber, projectileNumber;
    public bool rHandCollision, lHandCollision, wallCollision, goalCollision;
    public string rCollisionTime, lCollisionTime, wallCollisionTime, goalCollisionTime;
    public string deflected;
    public string projectileFiredTime;
    public string colliderSize, originalColliderSize;
    public bool startLogging,stopLogging;
    public bool tutorial;

    // Start is called before the first frame update
    void Start()
    {
        if(!tutorial){
        pg = gameObject.GetComponent<PathGenerator>();
        Invoke("GetPath", 1f);
        }
    }

    void GetPath(){
        writetext = new StreamWriter(pg.getPath());
        Log("Time, Set Number, Projectile Number, Right Hand Collision, Left Hand Collision, Wall Collision, Goal Collision, Right Hand Collision Time, Left Hand Collision Time, Wall Collision Time, Goal Collision Time, Deflected, Projectile Fired Time, Collider Size, Original Collider Size");
    }

   void Update(){
    if(tutorial)
        enabled = false;
    if (startLogging)
        Log(Time.time.ToString() + 
        "," + setNumber.ToString() + "," + projectileNumber.ToString() +
        "," + rHandCollision.ToString() + "," + lHandCollision.ToString() + "," + wallCollision.ToString() + "," + goalCollision.ToString() +
        "," + rCollisionTime + "," + lCollisionTime + "," + wallCollisionTime + "," + goalCollisionTime +
        "," + deflected + "," + projectileFiredTime +
        "," + colliderSize + "," + originalColliderSize + ",");
    
    if(stopLogging)writetext.Close();

    }

    void Log(string data)
    {
        writetext.WriteLine(data);
        rHandCollision = false;
        lHandCollision = false;
        wallCollision = false;
        goalCollision = false;
        rCollisionTime = "";
        lCollisionTime = "";
        wallCollisionTime = "";
        goalCollisionTime = "";
        deflected = "";
        projectileFiredTime="";
    }
}
