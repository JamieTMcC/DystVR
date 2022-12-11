using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShootLogger : MonoBehaviour
{

    private PathGenerator pg;
    public StreamWriter writetext;
    public int setNumber, targetNumber;
    public string trueshot,falseshot,aimCylinderSize,defaultAimCylinderSize;
    public string targetFiredTime, targetHitTime;
    public bool aimCylinderActive,startLogging,stopLogging;

    // Start is called before the first frame update
    void Start()
    {
        pg = gameObject.GetComponent<PathGenerator>();
        Invoke("GetPath", 1f);
    }

    void GetPath(){
        writetext = new StreamWriter(pg.getPath());
        Log("Time, Set Number, Target Number, False Shot, True Shot, Target Fired Time, Target Destroyed Time, Aim Cylinder Size, Normal Aim Cylinder Size, Aim Cylinder Active,");
    }

   void Update(){
    if (startLogging)
        Log(Time.time.ToString() + 
        "," + setNumber.ToString() + "," + targetNumber.ToString() + 
        "," + falseshot + "," + trueshot + 
        "," + targetFiredTime + "," + targetHitTime +
        "," + aimCylinderSize + "," + defaultAimCylinderSize + 
        "," + aimCylinderActive.ToString()+ ",");
    if(stopLogging)writetext.Close();

    }

    void Log(string data)
    {
        writetext.WriteLine(data);
        falseshot = "";
        trueshot = "";
        targetFiredTime="";
        targetHitTime="";
    }
}
