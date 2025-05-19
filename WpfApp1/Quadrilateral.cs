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
        public Point2D getP1()
        {
            return p;
        }
        public Point2D getP2()
        {
            return new Point2D(p.getX() + w, p.getY());
        }
        public Point2D getP3()
        {
            return new Point2D(p.getX() + w, p.getY() + h);
        }
        public Point2D getP4()
        {
            return new Point2D(p.getX(), p.getY() + h);
        }
    }
}
