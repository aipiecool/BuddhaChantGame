using RuntimeInspectorNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class CharacterCreatorView : MonoBehaviour
	{
        public Selector color_selector;
        public Selector gender_selector;      
        public Button enter_world_button;       
        public Text username_text;
        public ColorWheelControl colorWheel;
        public SpriteRenderer mainPlayer;

        private Sprite[] mCharacterSprites;
    

        private int mColorSelectorIndex = 0;
        private int mGenderSelectorIndex = 0;     

        private CharacterCreatorModel mModel;
        private void Start()
        {
            color_selector.addCallback(onColorSelectorClick);
            gender_selector.addCallback(onGenderSelectorClick);
            colorWheel.OnColorChanged += onColorChanged;

            mCharacterSprites = CharacterSpritesLoadUtils.loadCharacterSprites();            

            username_text.text = UserRecorder.get().getUsername();

            setColor(randomColor(), 0);
            setColor(randomColor(), 2);

            enter_world_button.onClick.AddListener(onEnterWorldButtonClick);

            mModel = new CharacterCreatorModel();
        }

        private void onEnterWorldButtonClick()
        {
            Dialog.get().show("您即将进入小镇。\n确定使用该角色形象吗?", ()=>
            {
                Loading.get().show();
                mModel.sendCharacterInfo(mGenderSelectorIndex, mainPlayer.color, ()=>
                {
                    CharacterInfoEntity entity = new CharacterInfoEntity();
                    entity.isCreated = true;
                    entity.gender = mGenderSelectorIndex;
                    entity.colorR = mainPlayer.color.r;
                    entity.colorG = mainPlayer.color.g;
                    entity.colorB = mainPlayer.color.b;
                    UserRecorder.get().setCharacterInfo(new CharacterInfo(entity));
                    SceneManager.LoadScene("Nianfotang");
                });
            });
        }

        private void onColorSelectorClick(int index)
        {
            mColorSelectorIndex = index;
        }

        private void onGenderSelectorClick(int index)
        {
            mGenderSelectorIndex = index;
            mainPlayer.sprite = mCharacterSprites[index];
        }

        private void onColorChanged(Color32 color32)
        {
            setColor(color32, mColorSelectorIndex);
        }        

        private void setColor(Color32 color32, int index)
        {
            Vector3 color = new Vector3((float)color32.r / 256, (float)color32.g / 256, (float)color32.b / 256) * 1000;
            float r = (color.x - color.x % 100);
            float g = (color.y / 10 - color.y / 10 % 10);
            float b = color.z / 1000;
            float compressValue = (r + g + b) / 1000;
            Color compressColor = mainPlayer.color;
            switch (index)
            {
                case 0:
                    compressColor.r = compressValue;
                    break;
                case 1:
                    compressColor.g = compressValue;
                    break;
                case 2:
                    compressColor.b = compressValue;
                    break;
            }
            mainPlayer.color = compressColor;
        }

        private Color randomColor()
        {
            float r = 1f, g = 1f, b = 1f;
            //定义3个颜色备用
            float c1 = 1f;
            float c2 = 150 / 255f;
            float c3 = Random.Range(150 / 255f, 1f);
            //将3个颜色随机分配给R,G,B
            int choose = Random.Range(0, 6);
            switch (choose)
            {
                case 0:
                    r = c1; g = c2; b = c3; break;
                case 1:
                    r = c1; g = c3; b = c2; break;
                case 2:
                    r = c2; g = c1; b = c3; break;
                case 3:
                    r = c2; g = c3; b = c1; break;
                case 4:
                    r = c3; g = c1; b = c2; break;
                case 5:
                    r = c3; g = c2; b = c1; break;
            }           
            return new Color(r, g, b);
        }
    }
}
