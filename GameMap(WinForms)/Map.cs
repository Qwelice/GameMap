using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GameMap_WinForms_
{
    /// <summary>
    /// Класс отрисовывающий карту
    /// </summary>
    class Map
    {
        // размеры карты
        private int _width = 0;
        private int _height = 0;

        // массив, содержащий информацию о карте
        private int[] map;

        public Map()
        {
            map = new int[]
            {
                1, 1, 1, 1, 1, 1,
                1, 0, 0, 1, 0, 1,
                1, 0, 0, 0, 1, 1,
                1, 0, 1, 0, 0, 1,
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

        /// <summary>
        /// Метод, рисующий карту
        /// </summary>
        /// <param name="g"> графический контекст, которым будет рисоваться карта </param>
        /// <param name="panelWidth"> ширина области, на которой будет нарисована карта </param>
        /// <param name="panelHeight"> высота области, на которой будет нарисована карта </param>
        public void DrawMap(Graphics g, int panelWidth, int panelHeight)
        {
            // очищаем область
            g.Clear(Color.White);

            // вычисляем ширину и высоту блока карты
            float blockWidth = (panelWidth * 1.0f) / _width;
            float blockHeight = (panelHeight * 1.0f) / _height;

            // изначальная позиция, относительно которой будут рисоваться блоки
            var pos = new PointF(0f, 0f);

            // изображение блока стены
            Image wall = makeBlock(BlockType.Wall, blockWidth, blockHeight);

            // изображение блока пола
            Image floor = makeBlock(BlockType.Floor, blockWidth, blockHeight);


            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    // если блок карты больше 0, то это стена
                    if (map[i * _width + j] > 0)
                        g.DrawImage(wall, pos.X, pos.Y);
                    // иначе это пол
                    else
                        g.DrawImage(floor, pos.X, pos.Y);
                    pos.X += blockWidth;
                }
                pos.X = 0;
                pos.Y += blockHeight;
            }
        }

        private Bitmap makeBlock(BlockType type, float blockWidth, float blockHeight)
        {
            return makeBlock(type, (int)blockWidth, (int)blockHeight);
        }

        private Bitmap makeBlock(BlockType type, int blockWidth, int blockHeight)
        {
            var block = new Bitmap(blockWidth, blockHeight);
            var gBlock = Graphics.FromImage(block);
            var b = new SolidBrush(Color.White);
            var p = new Pen(Color.Blue, 2.2f);
            switch (type)
            {
                case BlockType.Floor:
                    b.Color = Color.White;
                    break;
                case BlockType.Wall:
                    b.Color = Color.Black;
                    break;

                default:
                    break;
            }
            gBlock.FillRectangle(b, 0, 0, blockWidth, blockHeight);
            gBlock.DrawRectangle(p, 0, 0, blockWidth, blockHeight);
            return block;
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
                        map[index] = Convert.ToInt32(result[index].ToString());
                    }
                }
            }
        }
    }
}
