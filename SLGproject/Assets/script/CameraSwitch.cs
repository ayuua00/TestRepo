using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Transform cameraTs;
    [SerializeField] private Vector3 camera1_1;
    [SerializeField] private Vector3 camera1_2;
    [SerializeField] private float HightMin;
    [SerializeField] private float HightMax;
    [SerializeField] private float transTime = 1f;
    public bool capsLock;
    private bool isTransitioning = false;

    public float cameramoveSpeed = 20f;      // 移动速度
    float edgeSize = Screen.width * 0.02f;    // 边缘触发区域大小（像素）

    private Vector3 Distance;
    public Transform PlayerTs;

    private void Start()
    {
        Distance = PlayerTs.transform.position-cameraTs.transform.position;
        capsLock = true;
    }
    void Update()
    {
        CameraFollow();
        if (Input.GetKeyDown(KeyCode.CapsLock) && !isTransitioning)
        {
            Vector3 camera2_1 = new Vector3(cameraTs.position.x, HightMin, cameraTs.position.z);
            Vector3 camera2_2 = new Vector3(cameraTs.position.x, HightMax, cameraTs.position.z);
            Quaternion fromRotation = Quaternion.Euler(capsLock ? camera1_1 : camera1_2);
            Quaternion toRotation = Quaternion.Euler(capsLock ? camera1_2 : camera1_1);
            Vector3 fromPotion = (capsLock ? camera2_1 : camera2_2);
            Vector3 toPotion = (capsLock ? camera2_2 : camera2_1);


            StartCoroutine(TransitionRotation(fromRotation, toRotation,fromPotion,toPotion));
            capsLock = !capsLock;
        }

        if(capsLock==false)
        CameraMove();
    }

    IEnumerator TransitionRotation(Quaternion from01, Quaternion to01,Vector3 from02,Vector3 to02)
    {
        isTransitioning = true;
        float elapsed = 0f;

        while (elapsed < transTime)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / transTime);

            cameraTs.rotation = Quaternion.Slerp(from01, to01, t);
            cameraTs.position = Vector3.Slerp(from02, to02, t);
            yield return null;   // 关键
        }

        cameraTs.rotation = to01;  //防止t没到达1 结果不准确
        cameraTs.position = to02;  //防止t没到达1 
        isTransitioning = false; //不要在改变的过程中重复按
    }
    void CameraMove()
    {

        Vector3 moveDir = Vector3.zero;
        Vector3 mouse = Input.mousePosition;

        // 鼠标在屏幕左边缘
        if (mouse.x <= edgeSize)
            moveDir.z = -1;
        // 鼠标在屏幕右边缘
        else if (mouse.x >= Screen.width - edgeSize)
            moveDir.z = 1;

        // 鼠标在屏幕下边缘
        if (mouse.y <= edgeSize)
            moveDir.x = 1;
        // 鼠标在屏幕上边缘
        else if (mouse.y >= Screen.height - edgeSize)
            moveDir.x = -1;

        // 移动摄像机（世界坐标，xz 平面）
       cameraTs.transform.position += moveDir * cameramoveSpeed * Time.deltaTime;
    }
    void CameraFollow()
    {
        if (!capsLock) return;

        Vector3 targetPos = PlayerTs.position - Distance;
        cameraTs.position = Vector3.Lerp(
            cameraTs.position,
            targetPos,
            Time.deltaTime * 2f   // 每帧靠近一点
        );
    }
}
