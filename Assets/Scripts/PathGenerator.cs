using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;


public class PathGenerator : MonoBehaviour
{
    private string path;
    // Start is called before the first frame update

    void Start(){
        GenerateNewIteration();
    }

    void GenerateNewIteration()
    {  
        if(SceneManager.GetActiveScene().name == "ProjectileBlocker"){
            path = Application.persistentDataPath + "/experimentdata/ProjectileGame/";
        }else if(SceneManager.GetActiveScene().name == "PistolGameUnmodified"){
            path = Application.persistentDataPath + "/experimentdata/PistolGame/";
        }
        //Reads a number from a file and increments then writes to number each new user         
        int iteration;
        using(StreamReader readtext = new StreamReader(path + "iteration.txt")){
            iteration = Int32.Parse(readtext.ReadLine());
        }
        iteration++;
        using(StreamWriter writetext = new StreamWriter(path + "iteration.txt")){
            writetext.WriteLine(iteration.ToString());
        }
        path += iteration.ToString() + ".txt";

        using(StreamWriter writetext = new StreamWriter(path)){
            writetext.WriteLine("---------- New File ----------");
        }   
    }

    public string getPath(){
        return path;
    }
}
