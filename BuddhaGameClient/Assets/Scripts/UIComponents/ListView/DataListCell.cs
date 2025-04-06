using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BuddhaGame
{

	public abstract class DataListCell<T> : MonoBehaviour
	{
		private int mPosition;
		private OnDataCellClickCallback<T> mOnDataCellClickCallback;
		private T mData;
		private Image mImage;
		private bool mIsStarted = false;
		private Runable mOnInitializedCallback;

		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(onClick);
			mImage = GetComponent<Image>();
			mIsStarted = true;
			mOnInitializedCallback?.Invoke();
		}	

		public void setOnInitializedCallabck(Runable runable)
        {
			mOnInitializedCallback = runable;
            if (mIsStarted)
            {
				mOnInitializedCallback.Invoke();
			}
		}

		private void onClick()
		{
			mOnDataCellClickCallback?.Invoke(this, mPosition, mData);
		}

		public void setOnClickCallback(OnDataCellClickCallback<T> callback)
		{
			if(mOnDataCellClickCallback == null)
            {
				mOnDataCellClickCallback = callback;
            }
            else
            {
				mOnDataCellClickCallback += callback;

			}
			
		}

		public void onCreate(int position, T data)
		{			
			mPosition = position;
			mData = data;
			onCreate(data);
		}

		public void setColor(Color color)
        {
			mImage.color = color;
		}

		protected abstract void onCreate(T data);
	}
}
