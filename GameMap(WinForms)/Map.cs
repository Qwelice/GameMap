using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace GameMap_WinForms_
{
    class Map
    {
        private int _width = 0;
        private int _height = 0;

        private int[] map;

        public Map()
        {
            map = new int[]
            {
                1, 1, 1, 1, 1, 1,
                1, 0, 0, 1, 0, 1,
                1, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 1,
                1, 1, 1, 1, 1, 1,
            };
            _width = 6;
            _height = 5;
        }

        public Map(string path)
        {
            GetMap(path);
        }

        public int Width { get => _width; }

        public int Height { get => _height; }

        public void DrawMap(Graphics g, int panelWidth, int panelHeight)
        {
            float blockWidth = panelWidth / _width;
            float blockHeight = panelHeight / _height;
            var pos = new PointF(0f, 0f);
            var b = new SolidBrush(Color.White);
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (map[i * _width + j] == 1) b.Color = Color.Black;
                    else b.Color = Color.White;
                    g.FillRectangle(b, new RectangleF(pos, new SizeF(blockWidth, blockHeight)));
                    pos.X += blockWidth;
                }
                pos.X = 0;
                pos.Y += blockHeight;
            }
        }

        private void GetMap(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                string result = "";
                while(sr.Peek() != -1)
                {
                    result += sr.ReadLine();
                    _height++;
                    if (_height == 1) _width = result.Length;
                }
                map = new int[_width * _height];
                for(int i = 0; i < _height; i++)
                {
                    for(int j = 0; j < _width; j++)
                    {
                        int index = i * _width + j;
                        var str = Convert.ToInt32(result[index].ToString());
                        map[index] = str;
                    }
                }
            }
        }
    }
}
