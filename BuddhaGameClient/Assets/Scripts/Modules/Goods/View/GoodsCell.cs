using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
    public class GoodsCell : DataListCell<GameGoods>
    {

        public Text name_text;
        public Text count_text;

        protected override void onCreate(GameGoods data)
        {
            name_text.text = data.localName;
            count_text.text = data.count.ToString();
        }
    }
}
