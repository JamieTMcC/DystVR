using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public class CollideAndDestroy : MonoBehaviour
{

    public AudioSource Audiodata;

    static ReaderWriterLock locker = new ReaderWriterLock();

    public string Keyword = "";

    public void OnTriggerEnter(Collider collision) {

    using(StreamWriter writetext = new StreamWriter("write.txt"))
        {

        Audiodata.Play(0);


        if(collision.gameObject.tag == "Centre"){

            Keyword = "Centre";
            Destroy(collision.gameObject.transform.parent.gameObject);
            break;

        }else if (collision.gameObject.tag == "InnerRing"){
            
            Debug.Log("Hello");
            Keyword = "Inner Ring";
            Destroy(collision.gameObject.transform.parent.gameObject);
            break;

        }else if (collision.gameObject.tag == "OuterRing"){

            Keyword = "Outer Ring";
            Destroy(collision.gameObject);
            break;
        }


        try{
            locker.AcquireWriterLock(int.MaxValue);
            writetext.WriteLine(Keyword);
        }finally
        {
            locker.ReleaseWriterLock();
        }
        }
    }
}