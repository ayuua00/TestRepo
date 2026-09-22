using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croppingsystem : MonoBehaviour
{
    public InputManager_ InputM;
    public CameraSwitch Cs;
    public GameObject Plough;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Cs.capsLock == false && InputM.Gohit.transform.tag == "Grass"&&other.transform.tag=="Palyer") 
        {
            InputM.Gohit.SetActive(false);
            Instantiate(Plough);
            Plough.transform.position = InputM.Gohit.transform.position;
        }
    }
}
