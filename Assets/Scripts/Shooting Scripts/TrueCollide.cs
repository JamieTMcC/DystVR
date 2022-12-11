using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueCollide : MonoBehaviour
{

    private ShootLogger logger;
    string collisiontag;
    // Start is called before the first frame update
    void Start()
    {
        logger = GameObject.Find("XR Origin").GetComponent<ShootLogger>();
    }

    // Update is called once per frame
    void Update()
    {   
        Debug.Log(collisiontag);
    }
 public void OnTriggerEnter(Collider collision) {

        //not untagged is how we describe the different sections of the target
        if(collision.gameObject.tag == "InnerRing" || collision.gameObject.tag == "Centre" || collision.gameObject.tag == "OuterRing"){
            collisiontag = collision.gameObject.tag;
            logger.trueshot = collision.gameObject.tag;
        }
    }

}
