using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public delegate void OnDataCellClickCallback<T>(DataListCell<T> cell, int position, T data);
    public class DataAdapter<T, C> : ListView.Adapter where C : DataListCell<T>
    {        

        private List<T> mDatas;
        private OnDataCellClickCallback<T> mOnDataCellClickCallback;
        private OnListCellClickCallback mOnListCellClickCallback;

        public void setDatas(List<T> datas)
        {
            mDatas = datas;
            notifyDataChanged();
        }

        public override void onCreate(int position, GameObject gameObject)
        {
            C cell = gameObject.GetComponent<C>();
            cell.onCreate(position, mDatas[position]);
            cell.setOnClickCallback(onCellClick);
        }

        private void onCellClick(DataListCell<T> cell, int position, T data)
        {
            mOnDataCellClickCallback?.Invoke(cell, position, data);
            mOnListCellClickCallback?.Invoke(position);
        }

        public override int getSize()
        {
            return mDatas == null ? 0 : mDatas.Count;
        }

        public void setOnClickCallback(OnDataCellClickCallback<T> callback)
        {
            mOnDataCellClickCallback = callback;
        }

        public override void setOnClickCallback(OnListCellClickCallback callback)
        {
            mOnListCellClickCallback = callback;
        }

        public override void setCellColor(GameObject gameObject, Color color)
        {
            C cell = gameObject.GetComponent<C>();
            cell.setOnInitializedCallabck(()=>
            {
                cell.setColor(color);
            });
        }
    }
}
