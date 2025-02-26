using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickMove : MonoBehaviour
{
    public float maxRadius = 150;
    private RectTransform upperSprite;
    private Vector2 originAnchoredPosition;
    private Vector2 vector2Move = Vector2.zero;
    private bool isDrag = false;

    public delegate void OnMoveStart();
    public event OnMoveStart onMoveStart;
    public delegate void OnMoving(Vector2 vector2Move);
    public event OnMoving onMoving;
    public delegate void OnMoveEnd();
    public event OnMoveEnd onMoveEnd;

    public delegate void OnRotat(float rotatY);
    public event OnRotat onRotat;


    private Vector3 previousMousePosition;
    private bool isFirstClick;


    void Start()
    {
        this.upperSprite = transform.GetChild(0).GetComponentInChildren<RectTransform>();
        this.originAnchoredPosition = upperSprite.anchoredPosition;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //���ñ�����͸����
            // ��ȡ��ǰ��ɫ
            Color color = transform.GetComponent<Image>().color;
            // ����͸����Ϊ0
            color.a = 0.5f;
            // Ӧ���µ���ɫ
            transform.GetComponent<Image>().color = color;

            //������ק�����͸����
            Color color1 = transform.GetChild(0).GetComponentInChildren<Image>().color;
            // ����͸����Ϊ0
            color1.a = 1f;
            // Ӧ���µ���ɫ
            transform.GetChild(0).GetComponentInChildren<Image>().color = color;


            Vector3 MousePos=Input.mousePosition;
            upperSprite.anchoredPosition = new Vector3(0, 0, 0);
            transform.position=MousePos;
            this.isDrag = true;
            isFirstClick = true;
            if (this.onMoveStart != null)
            {
                onMoveStart();
            }
        }
        if(isDrag==true)
        {
            if(isFirstClick==true)
            {
                isFirstClick = false;
                return;
            }
            Vector3 currentMousePosition = Input.mousePosition;

            // �������λ�õı仯��
            Vector2 mouseDelta = currentMousePosition - previousMousePosition;

            // ������һ֡�����λ��
            previousMousePosition = currentMousePosition;

            this.upperSprite.anchoredPosition += mouseDelta;

            this.upperSprite.anchoredPosition = Vector2.ClampMagnitude(this.upperSprite.anchoredPosition, this.maxRadius);

            this.vector2Move = this.upperSprite.anchoredPosition / this.maxRadius;

            if (onMoving != null)
            {
                onMoving(this.vector2Move);
            }

            if (onRotat != null)
            {
                onRotat(-Vector2.SignedAngle(new Vector2(0, 1), this.vector2Move));
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            this.isDrag = false;
            this.upperSprite.anchoredPosition = this.originAnchoredPosition;
            if (onMoveEnd != null)
            {
                onMoveEnd();
            }
            // ��ȡ��ǰ��ɫ
            Color color = transform.GetComponent<Image>().color;
            // ����͸����Ϊ0
            color.a = 0f;
            // Ӧ���µ���ɫ
            transform.GetComponent<Image>().color = color;

            //������ק�����͸����
            Color color1 = transform.GetChild(0).GetComponentInChildren<Image>().color;
            // ����͸����Ϊ0
            color1.a = 0f;
            // Ӧ���µ���ɫ
            transform.GetChild(0).GetComponentInChildren<Image>().color = color;
        }
    }
    
}
