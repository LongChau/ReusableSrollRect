using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Key.Ultility;
using TMPro;

namespace ReusableScrollRect
{
    public class ChatBoxController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI txtChat;

        public RectTransform rectTransform;

        public Rect curRect;

        public bool IsInsideScrollView;

        public Rect parentRect;

        [SerializeField]
        private float width = 0.0f;
        [SerializeField]
        private float height = 0.0f;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        void Start()
        {
            width = rectTransform.position.x + rectTransform.rect.width;
            height = rectTransform.position.y + rectTransform.rect.height;
            curRect = new Rect(rectTransform.position.x, rectTransform.position.y, width, height);
        }

        public void SetChatText(string text)
        {
            txtChat.SetText(text);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSize();
            UpdateRect(rectTransform.position.x, rectTransform.position.y, width, height);
            CheckOverlap();
        }

        private void CheckOverlap()
        {
            //if (curRect.Overlaps(parentRect))
            if (GlobalFunc.IsRectOverlap(curRect, parentRect))  // well, don't know why unity "Overlaps" is incorrect. I rewrite this code
            {
                IsInsideScrollView = true;
            }
            else
                IsInsideScrollView = false;
        }

        private void UpdateSize()
        {
            width = rectTransform.position.x + rectTransform.rect.width;
            height = rectTransform.position.y + rectTransform.rect.height;
        }

        void UpdateRect(float x, float y, float width, float height)
        {
            curRect.x = x;
            curRect.y = y;
            curRect.width = width;
            curRect.height = height;
        }
    }
}
