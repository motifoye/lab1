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
        private Point2D p1;
        private Point2D p2;
        private Point2D p3;
        private Point2D p4;
        //Конструктор класса
        public Quadrilateral(Point2D p1, Point2D p2, Point2D p3, Point2D p4)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
        }
    }
}
