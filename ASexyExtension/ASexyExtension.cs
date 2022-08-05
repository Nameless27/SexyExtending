using SexyExtending;
using SexyExtending.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Drawing = System.Drawing;

namespace ASexyExtension
{
    public class ASexyExtension : SexyExtension<ASexyBehaviour>
    {
        public override Scenes BehaviourScenes => Scenes.Mian;

        public override string Id => "我_没_有_名_字.A sexy extension";

        public override string Name => "A Sexy Extension";

        public override string Description => "暂未完成";

        public override string Author => "我_没_有_名_字";

        public override string Version => "0.22.08.05";

        public override string Link => "https://space.bilibili.com/401593576";

        public override void OnExtensionLoaded()
        {
            GameWindow.Instance.Dwm.SetCornerRadius(WindowsCornerRadius.Zero);
            GameWindow.Instance.Dwm.SetCaption(Drawing.Color.FromArgb(24, 88, 100));
            GameWindow.Instance.Dwm.SetBorder(Drawing.Color.FromArgb(249, 166, 71));
            GameWindow.Instance.Dwm.SetText(Drawing.Color.FromArgb(249, 166, 71));
            GameWindow.Instance.Dwm.SetGlassFrameThickness(10, 10, 10, 10);
        }
    }

    public class ASexyBehaviour : MonoBehaviour
    {
        void OnGUI()
        {
            var rect = Rect.zero;
            if (modsManagerEnabled)
            {
                rect.Set(modsManagerX, modsManagerY, modsManagerWidth, modsManagerHeight);
                var modsManager = GUI.ModalWindow(0, rect, ModsManagerWindow_Function, "Mods");
            }
        }

        void ModsManagerWindow_Function(int id)
        {
            ScrollView(ModsList);
        }

        void ModsList(Vector2 value)
        {
            var rect = Rect.zero;
            rect.width = (modsManagerWidth / 3f) - 20f;
            rect.height = 20f;
            GUI.Button(rect, "1");
            rect.y += 20f;
            GUI.Button(rect, "2");
            rect.y += 20f;
            GUI.Button(rect, "3");
            rect.y += 20f;
            GUI.Button(rect, "4");
            rect.y += 20f;
            GUI.Button(rect, "5");
            rect.y += 20f;
            GUI.Button(rect, "6");
            rect.y += 20f;
            GUI.Button(rect, "7");
            rect.y += 20f;
            GUI.Button(rect, "8");
            rect.y += 20f;
            GUI.Button(rect, "9");
            rect.y += 20f;
            GUI.Button(rect, "10");
            rect.y += 20f;
            GUI.Button(rect, "11");
            rect.y += 20f;
            GUI.Button(rect, "12");
            rect.y += 20f;
            GUI.Button(rect, "13");
            rect.y += 20f;
            GUI.Button(rect, "14");
            rect.y += 20f;
            GUI.Button(rect, "15");
            rect.y += 20f;
            GUI.Button(rect, "16");
            rect.y += 20f;
            GUI.Button(rect, "17");
            rect.y += 20f;
            GUI.Button(rect, "18");
            rect.y += 20f;
            GUI.Button(rect, "19");
        }

        void ScrollView(Action<Vector2> gui)
        {
            var rect = Rect.zero;
            rect.Set(10, 20, modsManagerWidth / 3f, modsManagerHeight - 30f);
            var position = rect;
            rect.Set(10, 20, (modsManagerWidth / 3f) - 20f, modsManagerHeight - 30f);
            var scrollPosition = Vector2.zero;
            scrollPosition.x = rect.width - 10;
            scrollPosition.y = rect.y;
            var value = GUI.BeginScrollView(position, scrollPosition, rect);
            gui(value);
            GUI.EndScrollView();
        }

        void FixedUpdate()
        {
            if (!modsManagerEnabled)
                return;
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    modsManagerWidth += modsManagerSizeParam;
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        modsManagerHeight += modsManagerSizeParam;
                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        modsManagerHeight -= modsManagerSizeParam;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    modsManagerHeight += modsManagerSizeParam;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    modsManagerWidth -= modsManagerSizeParam;
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        modsManagerHeight += modsManagerSizeParam;
                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        modsManagerHeight -= modsManagerSizeParam;
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    modsManagerHeight -= modsManagerSizeParam;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    modsManagerX += modsManagerPositionParam;
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        modsManagerY += modsManagerPositionParam;
                    }
                    else if (Input.GetKey(KeyCode.UpArrow))
                    {
                        modsManagerY -= modsManagerPositionParam;
                    }
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    modsManagerY += modsManagerPositionParam;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    modsManagerX -= modsManagerPositionParam;
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        modsManagerY += modsManagerPositionParam;
                    }
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        modsManagerY -= modsManagerPositionParam;
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    modsManagerY -= modsManagerPositionParam;
                }
            }
        }
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    modsManagerEnabled = !modsManagerEnabled;
                }
            }
        }

        float modsManagerX = 20f;
        float modsManagerY = 20f;
        float _modsManagerWidth = 650f;
        float _modsManagerHeight = 400f;
        float modsManagerWidth
        {
            get => _modsManagerWidth;
            set
            {
                if (value > modsManagerMaxWidth)
                    return;
                if (value < modsManagerMinWidth)
                    return;
                _modsManagerWidth = value;
            }
        }
        float modsManagerHeight
        {
            get => _modsManagerHeight;
            set
            {
                if (value > modsManagerMaxHeight)
                    return;
                if (value < modsManagerMinHeight)
                    return;
                _modsManagerHeight = value;
            }
        }

        float modsManagerMaxWidth = Screen.width;
        float modsManagerMinWidth = 60f;
        float modsManagerMaxHeight = Screen.height;
        float modsManagerMinHeight = 45f;

        float modsManagerPositionParam = 4f;
        float modsManagerSizeParam = 4f;

        bool modsManagerEnabled = false;
    }
}
