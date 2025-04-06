using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class GoodsView : UIHideable
	{

        private static GoodsView sInstance;

        public ListView goodsList;      

        private GoodsModel mGoodsModel = new GoodsModel();
        private DataAdapter<GameGoods, GoodsCell> mDataAdapter = new DataAdapter<GameGoods, GoodsCell>();

        private void Start()
        {
            sInstance = this;
            goodsList.setAdapter(mDataAdapter);
            mDataAdapter.setOnClickCallback((cell, postion, data) =>
            {
               
            });
        }

        public static GoodsView get()
        {
            return sInstance;
        }

        public override void show()
        {
            mGoodsModel.requestGoods((gameGoods) =>
            {
                ThreadLooper.get().runMainThread(() =>
                {
                    mDataAdapter.setDatas(gameGoods);
                    if (gameGoods.Count > 0)
                    {
                        goodsList.setActiveCell(0);                       
                    }
                });
            });
            base.show();
        }
    }
}
