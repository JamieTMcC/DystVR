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
    
    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "InnerRing" || collision.gameObject.tag == "Centre" || collision.gameObject.tag == "OuterRing"){
            collisiontag = collision.gameObject.tag;
            logger.trueshot = collision.gameObject.tag;
        }
    }

}
