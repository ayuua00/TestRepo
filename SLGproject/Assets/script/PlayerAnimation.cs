using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAnimation : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorChange();
    }

    void  AnimatorChange()
    {
        
        bool isMove = agent.velocity.magnitude > 0.1f;
        Anim.SetBool("IsMove", isMove);
    }
}
