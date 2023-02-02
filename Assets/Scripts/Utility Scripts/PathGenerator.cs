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
    public bool done;
    
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
        
        NoteScene();

        path += SceneManager.GetActiveScene().name + "/";
        
        if(!Directory.Exists(path)){
            Directory.CreateDirectory(path);
            using(StreamWriter writetext = new StreamWriter(path + "iteration.txt")){
            writetext.WriteLine("0");
        }
        }
        Debug.Log(path);


        //Reads a number from a file and increments then writes to number each new user         
        int iteration;
        using(StreamReader readtext = new StreamReader(path + "iteration.txt")){
            iteration = Int32.Parse(readtext.ReadLine());
        }
        iteration++;
        using(StreamWriter writetext = new StreamWriter(path + "iteration.txt")){
            writetext.WriteLine(iteration.ToString());
        }
        path += iteration.ToString() + ".csv";
        Debug.Log(path);
        done = true;
    }

    public string getPath(){
        return path;
    }

    private void NoteScene(){
        using(StreamWriter writetext = new StreamWriter(Application.persistentDataPath + "/experimentdata/scenes.txt", true)){
            writetext.WriteLine(SceneManager.GetActiveScene().name);
        }
        //reads the number of lines in the file and prints it to the console
        int lineCount = 0;
        using(StreamReader readtext = new StreamReader(Application.persistentDataPath + "/experimentdata/scenes.txt")){
            while(readtext.ReadLine() != null){
                lineCount++;
            }
        }
        Debug.Log(lineCount);
        if(lineCount > 4){
            //quit game if player has done all 4 scenes
            Debug.Log("Games Quits Here");
            Application.Quit();
        }
    }
}
