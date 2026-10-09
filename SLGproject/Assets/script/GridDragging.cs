using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDragging: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float dragAlpha = 0.6f;//拖拽时的透明度

    private Transform itemIcon;// 记录当前格子里装的物品图标
    private CanvasGroup canvasGroup; // 控制图标的射线和透明度
    private Transform originalParent; // 记录图标原来属于哪个格子
    private Vector3 originalPosition; // 记录图标原来的本地坐标
    void Awake()
    {
        // 假设物品图标是格子的第一个子物体
        if (transform.childCount > 0)
        {
            itemIcon = transform.GetChild(0);
            // 确保物品图标上有 CanvasGroup 组件
            canvasGroup = itemIcon.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = itemIcon.gameObject.AddComponent<CanvasGroup>();
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)//开始拖拽
    {
        if (itemIcon == null) return; // 如果格子里没东西，不让他拖

        originalParent = itemIcon.parent;
        originalPosition = itemIcon.localPosition;//记录初始状态

        itemIcon.SetParent(GetComponentInParent<Canvas>().transform);// 把图标临时移出原来的格子，放到 Canvas 的最上层，防止被其他 UI 挡住

        // 降低透明度，并关闭射线检测（这样鼠标才能穿透它，检测到下面的格子）
        canvasGroup.alpha = dragAlpha;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)//拖拽过程中
    {
        Vector2 globalMousePos;//接受转换后的世界坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(itemIcon.GetComponent<RectTransform>(),
            eventData.position,eventData.pressEventCamera,out globalMousePos))
        {
            itemIcon.localPosition = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)//结束拖拽
    {
        if (itemIcon == null) return;

        // 恢复图标的属性
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // 发射一条 UI 射线，检测鼠标松手时下方的所有 UI 物体
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        GridDragging targetSlot = null;

        // 遍历射线打到的所有物体，看看有没有 InventorySlot 脚本
        foreach (RaycastResult result in results)
        {
            targetSlot = result.gameObject.GetComponent<GridDragging>();
            if (targetSlot != null && targetSlot != this) // 找到了另一个格子
            {
                break;
            }
        }

        // --- 开始判断 ---
        if (targetSlot != null)
        {
            // 情况 A：拖到了另一个有效的格子上
            Debug.Log("拖拽到了另一个格子，开始交换物品！");
            SwapItems(targetSlot);
        }
        else
        {
            // 情况 B：拖到了空白处或无效区域，弹回原位
            Debug.Log("拖拽无效，弹回原位。");
            itemIcon.SetParent(originalParent);
            itemIcon.localPosition = originalPosition;
        }
    }

    // 交换两个格子里的物品
    private void SwapItems(GridDragging targetSlot)
    {
        Transform targetItemIcon = null;
        if (targetSlot.transform.childCount > 0)
        {
            targetItemIcon = targetSlot.transform.GetChild(0);
        }

        // 1. 把当前拖拽的图标，塞进目标格子里
        itemIcon.SetParent(targetSlot.transform);
        itemIcon.localPosition = Vector3.zero; // 归零，使其在格子正中间

        // 2. 如果目标格子里原本有东西，把它拿过来塞进当前格子里
        if (targetItemIcon != null)
        {
            targetItemIcon.SetParent(originalParent);
            targetItemIcon.localPosition = Vector3.zero;
        }
    }
}