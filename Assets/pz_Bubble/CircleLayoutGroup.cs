using UnityEngine;
using System.Collections.Generic;

namespace SK.Framework.UI
{
    /// <summary>
    /// Բ���Զ��������
    /// </summary>
    public class CircleLayoutGroup : MonoBehaviour
    {
        //�뾶
        [SerializeField] private float radius = 100f;
        //�ǶȲ�
        [SerializeField] private float angleDelta = 30f;
        //��ʼ�ķ��� 0-Right 1-Up 2-Left 3-Down
        [SerializeField] private int startDirection = 0;
        //�Ƿ��Զ�ˢ�²���
        [SerializeField] private bool autoRefresh = true;
        //�Ƿ����������Ĵ�С
        [SerializeField] private bool controlChildSize = true;
        //�������С
        [SerializeField] private Vector2 childSize = Vector2.one * 100f;

        //��������������
        private int cacheChildCount;

        private void Start()
        {
            cacheChildCount = transform.childCount;
            RefreshLayout();
        }

        private void Update()
        {
            //�����Զ�ˢ��
            if (autoRefresh)
            {
                //��⵽�����������䶯
                if (cacheChildCount != transform.childCount)
                {
                    //ˢ�²���
                    RefreshLayout();
                    //�ٴλ�������������
                    cacheChildCount = transform.childCount;
                }
            }
        }

        /// <summary>
        /// ˢ�²���
        /// </summary>
        public void RefreshLayout()
        {
            //��ȡ���з�����״̬��������
            List<RectTransform> children = new List<RectTransform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.gameObject.activeSelf)
                {
                    children.Add(child as RectTransform);
                }
            }
            //�γɵ����εĽǶ� = �������϶���� * �ǶȲ�
            float totalAngle = (children.Count - 1) * angleDelta;
            //�ܽǶȵ�һ��
            float halfAngle = totalAngle * 0.5f;
            //������Щ������
            for (int i = 0; i < children.Count; i++)
            {
                RectTransform child = children[i];
                /* ���Ҳ�Ϊ0����� 
                 * ����ΪUpʱ�Ƕ�+90 Left+180 Down+270
                 * ����ΪRight��Upʱ �������Ƕ� 
                 * ȷ���㼶�е������尴�մ����ҡ����ϵ��µ�˳���Զ����� */
                float angle = angleDelta * (startDirection < 2 ? children.Count - 1 - i : i) - halfAngle + startDirection * 90f;
                //����x��y����
                float x = radius * Mathf.Cos(angle * Mathf.PI / 180f);
                float y = radius * Mathf.Sin(angle * Mathf.PI / 180f);
                //Ϊ�����帳ֵ����
                Vector2 anchorPos = child.anchoredPosition;
                anchorPos.x = x;
                anchorPos.y = y;
                child.anchoredPosition = anchorPos;

                //�����������С
                if (controlChildSize)
                {
                    child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childSize.x);
                    child.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childSize.y);
                }
            }
        }
    }
}
