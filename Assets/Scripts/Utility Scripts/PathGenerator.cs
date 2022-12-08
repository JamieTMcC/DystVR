/*
Handles the file management by creating a file at runtime and generating a path
*/
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
        path = Application.persistentDataPath + "/experimentdata/";

        if(!Directory.Exists(path)){
            Directory.CreateDirectory(path);
        }

        if(SceneManager.GetActiveScene().name == "ProjectileBlocker"){
            path += "ProjectileGame/";
        }else if(SceneManager.GetActiveScene().name == "PistolGameUnmodified"){
            path += "PistolGame/";
        }
        
        if(!Directory.Exists(path)){
            Directory.CreateDirectory(path);
            using(StreamWriter writetext = new StreamWriter(path + "iteration.txt")){
            writetext.WriteLine("0");
        }
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
    }

    public string getPath(){
        return path;
    }
}
