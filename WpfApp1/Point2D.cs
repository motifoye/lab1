using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Point2D
    {
        //Атрибуты класса
        private int X;
        private int Y;
        //Конструктор класса
        public Point2D(int x, int y)
        {
        //this используется для однозначного указания на атрибуты класса так как переменные имеют одинаковые имена
            this.X = x;
            this.Y = y;
        }
        //Методы для получения координат
        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
        //Методы для изменения координат
        public void addX(int x)
        {
            X += x;
        }
        public void addY(int y)
        {
            Y += y;
        }
    }
}
