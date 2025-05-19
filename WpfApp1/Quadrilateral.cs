using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Quadrilateral
    {
        //Атрибуты класса
        private Point2D p;
        private int w;
        private int h;

        //Конструктор класса
        public Quadrilateral(Point2D p, int w, int h)
        {
            this.p = p;
            this.w = w;
            this.h = h;
        }
        // Точка слева сверху
        public Point2D getP1()
        {
            return p;
        }
        // Точка справа сверху
        public Point2D getP2()
        {
            return new Point2D(p.getX() + w, p.getY());
        }
        // Точка справа снизу
        public Point2D getP3()
        {
            return new Point2D(p.getX() + w, p.getY() + h);
        }
        // Точка слева снизу
        public Point2D getP4()
        {
            return new Point2D(p.getX(), p.getY() + h);
        }
    }
}
