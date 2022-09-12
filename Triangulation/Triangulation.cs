using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Triangulation
{
    class Triangulation
    {
        public List<PointD> nodes;
        public List<Triangle> triangles;

        private readonly int N;
        private readonly Random rnd;
        private PointD newNode;

        public Triangulation(int N)
        {
            this.N = N;
            rnd = new Random(DateTime.Now.Millisecond);

            nodes = new List<PointD>();
            triangles = new List<Triangle>();

            RecurrentDelaunayTriangulation();
        }

        /// <summary>
        /// Триангуляция методом простого перебора с правилом Делоне.
        /// </summary>
        private void DelaunayTriangulation(List<PointD> nodes, bool isInit = true)
        {
            for (int i = 0; i < nodes.Count; i++)
                for (int j = i + 1; j < nodes.Count; j++)
                    for (int k = j + 1; k < nodes.Count; k++)
                    {
                        // Выбор тройки узлов.
                        Triangle triangle = new Triangle(nodes[i], nodes[j], nodes[k]);
                        bool isCheckDelaunay = true;
                        // Проверка Делоне.
                        for (int p = 0; p < nodes.Count; p++)
                        {
                            if (p == i || p == j || p == k)
                                continue;

                            if (!triangle.IsСheckingDelaunay(nodes[p]))
                            {
                                isCheckDelaunay = false;
                                break;
                            }
                        }

                        if (isInit == false)
                        {
                            // Проверка на то, что в треугольнике есть новый узел.
                            if (isCheckDelaunay == true && triangle.IsNewNode(newNode))
                                triangles.Add(triangle);
                        }                        
                        else if (isCheckDelaunay == true)   // Условие, для начальной триангуляции.
                                triangles.Add(triangle);
                    }
        }

        /// <summary>
        /// Реккурентная процедура триангуляции.
        /// </summary>
        private void RecurrentDelaunayTriangulation()
        {
            // Добавление точек сверхструктуры.
            nodes.Add(new PointD(0, 0));
            nodes.Add(new PointD(0, 1));
            nodes.Add(new PointD(1, 0));
            nodes.Add(new PointD(1, 1));
            nodes.Add(new PointD(rnd.NextDouble(), rnd.NextDouble()));
            // Начальная триангуляция Делоне.
            DelaunayTriangulation(nodes);

			for (int p = 0; p < N - 1; p++)
			{
				// Добавление нового узла.
				newNode = new PointD(rnd.NextDouble(), rnd.NextDouble());
				nodes.Add(newNode);
				List<PointD> nodes2 = new List<PointD>();
				// Проверка Делоне.
				for (int t = 0; t < triangles.Count; t++)
					if (!triangles[t].IsСheckingDelaunay(newNode))
					{
						nodes2.AddRange(triangles[t].Nodes());      // Добавляю вершины удалённых треугольников в список. 
						triangles.Remove(triangles[t]);             // Удаляю треугольники.
						t--;
					}
				nodes2.Add(newNode);
				DelaunayTriangulation(nodes2.Distinct().ToList(), false);
			}
		}
    }

    public class Triangle
    {
        public PointD P1;
        public PointD P2;
        public PointD P3;

        /// <summary>
        /// Центр масс треугольника (центр описанной окружности).
        /// </summary>
        public PointD Pc
        {
            get
            {
                // Вычисление центра описанной окружности.
                double Z1 = P1.X * P1.X + P1.Y * P1.Y;
                double Z2 = P2.X * P2.X + P2.Y * P2.Y;
                double Z3 = P3.X * P3.X + P3.Y * P3.Y;
                double Zx = (P1.Y - P2.Y) * Z3 + (P2.Y - P3.Y) * Z1 + (P3.Y - P1.Y) * Z2;
                double Zy = (P1.X - P2.X) * Z3 + (P2.X - P3.X) * Z1 + (P3.X - P1.X) * Z2;
                double Z = 2 * ((P1.X - P2.X) * (P3.Y - P1.Y) - (P1.Y - P2.Y) * (P3.X - P1.X));
                // Вычисление радиуса описанной окружности.
                return new PointD(-Zx / Z, Zy / Z);
            }
            private set { }
        }

        /// <summary>
        /// Радиус описанной окружности.
        /// </summary>
        public double Rc
        {
            get
            {
                return PointD.Distance(Pc, P1);
            }
            private set { }
        }

        /// <summary>
        /// Конструктор создания треугольника.
        /// </summary>
        /// <param name="p1">Первая вершина.</param>
        /// <param name="p2">Вторая вершина.</param>
        /// <param name="p3">Третья вершина.</param>
        public Triangle(PointD p1, PointD p2, PointD p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        /// <summary>
        /// Возвращает список вершин треугольника.
        /// </summary>
        /// <returns></returns>
        public List<PointD> Nodes()
        {
            List<PointD> nodes = new List<PointD>();
            nodes.Add(P1);
            nodes.Add(P2);
            nodes.Add(P3);

            return nodes;
        }

        /// <summary>
        /// Проверка Делоне.
        /// </summary>
        /// <param name="point">Проверяемая точка.</param>
        /// <returns></returns>        
        public bool IsСheckingDelaunay(PointD point)
        {
            return PointD.Distance(Pc, point) > Rc;
        }

        public bool IsNewNode(PointD newNode)
        {
            return (P1 == newNode || P2 == newNode || P3 == newNode);
        }
    }

    class DrawingTriangulation
    {
        private readonly PictureBox pictureBox;
        private readonly Graphics graphics;
        private readonly Bitmap bitmap;

        private readonly double wnd_Xmin, wnd_Xmax, wnd_Ymin, wnd_Ymax;
        private readonly double alpha, beta;

        public DrawingTriangulation(PictureBox pB, double x1, double y1, double x2, double y2)
        {
            pictureBox = pB;
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Относительные размеры окна рисования.
            wnd_Xmin = x1; wnd_Xmax = x2;
            wnd_Ymin = y1; wnd_Ymax = y2;
            // Вычисление коэффициентов преобразование.
            alpha = pictureBox.Width / (wnd_Xmax - wnd_Xmin);
            beta = pictureBox.Height / (wnd_Ymax - wnd_Ymin);

            pictureBox.Image = bitmap;
        }

        /// <summary>
        /// Очищает область.
        /// </summary>
        public void Clear()
        {
            graphics.Clear(Color.Black);
        }

        /// <summary>
        /// Преобразует мировые координаты в координаты окна (пиксели).
        /// </summary>
        /// <param name="x">X в мировых координатах.</param>
        /// <returns>Координата, преобразованная в координаты окна (пиксели).</returns>
        private float OutX(double x)
        {
            return (float)(x * alpha);
        }

        /// <summary>
        /// Преобразует мировые координаты в координаты окна (пиксели).
        /// </summary>
        /// <param name="y">Y мировых координатах.s</param>
        /// <returns>Координата, преобразованная в координаты окна (пиксели).</returns>
        private float OutY(double y)
        {
            return (float)(y * beta);
        }

        /// <summary>
        /// Рисует прямую линию между двумя заданными точками.
        /// </summary>
        /// <param name="color">Цвет линии.</param>
        /// <param name="x1">X-положение начальной точки в мировых координатах.</param>
        /// <param name="y1">Y-положение начальной точки в мировых координатах.</param>
        /// <param name="x2">X-положение конечной точки в мировых координатах.</param>
        /// <param name="y2">Y-положение конечной точки в мировых координатах.</param>
        private void DrawLine(Color color, double x1, double y1, double x2, double y2, float width = 1f)
        {
            Pen pen = new Pen(color);
            pen.Width = width;
            pen.DashStyle = DashStyle.Solid;
            graphics.DrawLine(pen, OutX(x1), OutY(y1), OutX(x2), OutY(y2));
        }

        /// <summary>
        /// Рисует эллипс.
        /// </summary>
        /// <param name="x">X-положение начальной точки в мировых координатах.</param>
        /// <param name="y">Y-положение начальной точки в мировых координатах.</param>
        /// <param name="width">Ширина эллипса в мировых координатах.</param>
        /// <param name="height">Высота эллипса в мировых координатах.</param>
        private void DrawEllipse(Color color, double x, double y, double width, double height)
        {
            Pen pen = new Pen(color);
            pen.Width = 1f;
            pen.DashStyle = DashStyle.Solid;
            graphics.DrawEllipse(pen, OutX(x) - OutX(width) / 2, OutY(y) - OutY(height) / 2, OutX(width), OutY(height));
        }
        /// <summary>
        /// Отрисовка триангуляции.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="triangles"></param>
        public void DrawTriangulation(List<PointD> nodes, List<Triangle> triangles)
        {
            // Отрисовка треугольников.
            for (int i = 0; i < triangles.Count; i++)
            {
                DrawLine(Color.White, triangles[i].P1.X, triangles[i].P1.Y, triangles[i].P2.X, triangles[i].P2.Y);
                DrawLine(Color.White, triangles[i].P1.X, triangles[i].P1.Y, triangles[i].P3.X, triangles[i].P3.Y);
                DrawLine(Color.White, triangles[i].P2.X, triangles[i].P2.Y, triangles[i].P3.X, triangles[i].P3.Y);
            }
            // Отрисовка описанной окружности.
            //for (int i = 0; i < triangles.Count; i++)
            //	DrawEllipse(Color.Red, triangles[i].Pc.X, triangles[i].Pc.Y, 2 * triangles[i].Rc, 2 * triangles[i].Rc);
        }
    }

    /// <summary>
    /// Структура точек Double.
    /// </summary>
    public struct PointD
    {
        public double X;
        public double Y;
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
        public override bool Equals(object obj)
        {
            return obj is PointD d && this == d;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
        public static bool operator ==(PointD a, PointD b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(PointD a, PointD b)
        {
            return !(a == b);
        }
        public static double Distance(PointD p1, PointD p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}