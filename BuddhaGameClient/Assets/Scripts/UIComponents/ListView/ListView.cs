using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public delegate void OnDataChangedCallback();
    public delegate void OnListCellClickCallback(int position);

    public class ListView : MonoBehaviour
	{
        public GameObject cell;
        public Color activeColor;
        public Color unactiveColor;

        private Transform mContent;
        private Adapter mAdapter;

        private void Start()
        {
            mContent = transform.GetChild(0).GetChild(0).transform;
        }

        public void setAdapter(Adapter adapter)
        {
            mAdapter = adapter;
            mAdapter.setDataChangedCallback(onAdapterDataChanged);
            mAdapter.setOnClickCallback(onCellClick);
        }

        private void onCellClick(int position)
        {
            setActiveCell(position);
        }

        public void setActiveCell(int position)
        {
            for (int i = 0; i < mContent.childCount; i++)
            {
                GameObject gameObject = mContent.GetChild(i).gameObject;
                if (position == i)
                {
                    mAdapter.setCellColor(gameObject, activeColor);
                }
                else
                {
                    mAdapter.setCellColor(gameObject, unactiveColor);
                }
            }            
        }

        private void onAdapterDataChanged()
        {
            clearAllCell();
            int size = mAdapter.getSize();
            float height = 0;
            for(int i=0; i<size; i++)
            {
                GameObject gameObject = Instantiate(cell);
                gameObject.transform.SetParent(mContent);
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                float cellHeight = rectTransform.rect.height;
                rectTransform.offsetMin = new Vector2(0, height);         
                rectTransform.offsetMax = new Vector2(0, cellHeight);
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cellHeight);
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, height);
                rectTransform.localScale = Vector3.one;
                height -= cellHeight;
                mAdapter.onCreate(i, gameObject);
            }
        }

        public void clearAllCell()
        {
            for (int i = 0; i < mContent.childCount; i++)
            {
                DestroyImmediate(mContent.GetChild(0).gameObject);
            }
        }

        public abstract class Adapter
        {
            private OnDataChangedCallback mOnDataChangedCallback;

            public void setDataChangedCallback(OnDataChangedCallback onAdapterDataChanged)
            {
                mOnDataChangedCallback = onAdapterDataChanged;
            }

            public void notifyDataChanged()
            {
                mOnDataChangedCallback?.Invoke();
            }

            public abstract void onCreate(int position, GameObject gameObject);

            public abstract int getSize();

            public abstract void setOnClickCallback(OnListCellClickCallback callback);

            public abstract void setCellColor(GameObject gameObject, Color color);
        }        

        
    }
}
