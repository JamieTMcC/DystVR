using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Logger : MonoBehaviour
{

    public PathGenerator pg = new PathGenerator();
    public string collided;
    public string time;
    public vector3 lhandpos, rhandpos, trajectoryPos;




    private string path;

    // Start is called before the first frame update
    void Start()
    {
        path = pg.getPath();
    }

    Update(){
        lhandpos = GameObject.Find("Left Hand").transform.position;
        rhandpos = GameObject.Find("Right Hand").transform.position;
        trajectoryPos = GameObject.Find("Cylinder").transform.position;
        time = Time.time.ToString();
        Log(time + "," + lhandpos + "," + rhandpos + "," + collided +"");
    }

    void Log(string data, bool newline = false, bool additive = false)
    {
        using(StreamWriter writetext = new StreamWriter(path)){
            if(additive){
                writetext.Write(time + "," lhandpos + "," rhandpos+ "," + trajectoryPos+ "," + collided "," + data);
            }
            writetext.WriteLine(data);
        }
        using(StreamWriter writetext = new StreamWriter(path)){
            if(newline){
                writetext.WriteLine("\n");
            }
        }
    }
}
