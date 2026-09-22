using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MoveToClickPoint : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //定义的一个容器 用来存储射线发射后击中到的物体的信息
            RaycastHit hit;

            //Phiysics.Raycast（射线的发射起点，射线发射的方向，这条射线击中的物体信息要存储到哪个容器，射线发射的最大长度距离，检测层，是否忽略触发器）
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }
}
