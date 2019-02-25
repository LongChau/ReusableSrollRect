using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Key.Ultility;
using System;

namespace ReusableScrollRect
{
    public class ScrollViewController : MonoBehaviour
    {
        private ScrollRect scrollRect;

        [SerializeField]
        private Scrollbar scrollBar;

        [SerializeField]
        private Rect curRect;

        [SerializeField]    // for DEBUG only
        private ChatBoxController[] arrChatBoxes;

        [SerializeField]
        private RectTransform contentRectTransform;
        private RectTransform curRectTransform;

        [SerializeField]
        private float width = 0.0f;
        [SerializeField]
        private float height = 0.0f;

        private float lastValue;

        public bool IsScrollingUp;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();

            curRectTransform = GetComponent<RectTransform>();

            width = curRectTransform.position.x + curRectTransform.rect.width;
            height = curRectTransform.position.y + curRectTransform.rect.height;
            curRect = new Rect(curRectTransform.position.x, curRectTransform.position.y, width, height);

            arrChatBoxes = new ChatBoxController[contentRectTransform.childCount];
            for (int chatBoxIndex = 0; chatBoxIndex < contentRectTransform.childCount; chatBoxIndex++)
            {
                arrChatBoxes[chatBoxIndex] = contentRectTransform.GetChild(chatBoxIndex).GetComponent<ChatBoxController>();
                arrChatBoxes[chatBoxIndex].SetChatText(ChatData.data[chatBoxIndex]);
                arrChatBoxes[chatBoxIndex].parentRect = curRect;
            }

        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnEnable()
        {
            //Subscribe to the ScrollRect event
            //scrollRect.onValueChanged.AddListener(ScrollRect_OnValueChanged);
            scrollBar.onValueChanged.AddListener(ScrollBar_OnValueChanged);
            lastValue = scrollBar.value;
        }

        private void ScrollBar_OnValueChanged(float value)
        {
            if (lastValue > value)
            {
                //Log.Info("Scrolling UP: " + value);
                IsScrollingUp = true;
            }
            else
            {
                //Log.Info("Scrolling DOWN: " + value);
                IsScrollingUp = false;
            }
            lastValue = value;
        }

        //Will be called when ScrollRect changes
        void ScrollRect_OnValueChanged(Vector2 value)
        {
            Debug.Log("ScrollRect Changed: " + value);
        }

        void OnDisable()
        {
            //Un-Subscribe To ScrollRect Event
            //scrollRect.onValueChanged.RemoveListener(ScrollRect_OnValueChanged);
            scrollBar.onValueChanged.RemoveListener(ScrollBar_OnValueChanged);
        }
    }
}