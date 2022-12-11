using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProjectileLogger : MonoBehaviour
{
    private PathGenerator pg;
    public StreamWriter writetext;
    public int setNumber, projectileNumber;
    public bool rHandCollision, lHandCollision, wallCollision;
    public string rCollisionTime, lCollisionTime, wallCollisionTime;
    public string deflected;
    public string projectileFiredTime, projectilePassedTime;
    public string colliderSize, originalColliderSize;
    public bool startLogging,stopLogging;

    // Start is called before the first frame update
    void Start()
    {
        pg = gameObject.GetComponent<PathGenerator>();
        Invoke("GetPath", 1f);
    }

    void GetPath(){
        writetext = new StreamWriter(pg.getPath());
        Log("Time, Set Number, Projectile Number, Right Hand Collision, Left Hand Collision, Wall Collision, Right Collision Time, Left Collision Time, Wall Collision Time, Deflected, Projectile Fired Time, Projectile Passed Time, Collider Size, Original Collider Size,");
    }

   void Update(){
    if (startLogging)
        Log(Time.time.ToString() + 
        "," + setNumber.ToString() + "," + projectileNumber.ToString() +
        "," + rHandCollision.ToString() + "," + lHandCollision.ToString() + "," + wallCollision.ToString() +
        "," + rCollisionTime + "," + lCollisionTime + "," + wallCollisionTime +
        "," + deflected + "," + projectileFiredTime + "," + projectilePassedTime +
        "," + colliderSize + "," + originalColliderSize + ",");
    
    if(stopLogging)writetext.Close();

    }

    void Log(string data)
    {
        writetext.WriteLine(data);
        rHandCollision = false;
        lHandCollision = false;
        wallCollision = false;
        rCollisionTime = "";
        lCollisionTime = "";
        wallCollisionTime = "";
        deflected = "";
        projectileFiredTime="";
        projectilePassedTime="";
    }
}
