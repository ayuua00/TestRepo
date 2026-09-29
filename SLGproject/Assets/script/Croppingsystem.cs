using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croppingsystem : MonoBehaviour
{
    public InputManager_ InputM;
    public CameraSwitch Cs;
    public GameObject Plough;

    public Transform player;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cropping();
    }

    public void Cropping()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Cs.capsLock == false && InputM.Gohit.transform.tag == "Grass") 
        {
            float distance = (InputM.Gohit.transform.position - player.transform.position).sqrMagnitude;
            if (distance < 25f)
            {
                InputM.Gohit.SetActive(false);
                Instantiate(Plough);
                Plough.transform.position = InputM.Gohit.transform.position;
            }
        }
    }
}
