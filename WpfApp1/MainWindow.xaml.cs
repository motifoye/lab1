using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Triangle tr;
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        //функция в основном теле программы
        public void DrawLine(Point2D p1, Point2D p2)
        {
            //Создание новой линии
            Line line = new Line();
            //Цвет и толщина линии
            line.Stroke = Brushes.Red;
            line.StrokeThickness = 3;
            //Установка координат линии из координат точек Point2D
            line.X1 = p1.getX();
            line.Y1 = p1.getY();
            line.X2 = p2.getX();
            line.Y2 = p2.getY();
            //Добавление линии в Canvas
            Scene.Children.Add(line);
        }

        public void DrawTriangle(Triangle tr)
        {
            //Отрисовка треугольника с помощью функции отрисовки линии
            DrawLine(tr.getP1(), tr.getP2());
            DrawLine(tr.getP2(), tr.getP3());
            DrawLine(tr.getP3(), tr.getP1());
        }

        public void DrawQuadrilateral(Quadrilateral qu)
        {
            DrawLine(qu.getP1(), qu.getP2());
            DrawLine(qu.getP2(), qu.getP3());
            DrawLine(qu.getP3(), qu.getP4());
            DrawLine(qu.getP4(), qu.getP1());
        }

        // events

        public void DrawTriangleEv(object s, RoutedEventArgs e)
        {
            //Создание треугольника со случайными координатами
            Point2D p1 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            Point2D p2 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            Point2D p3 = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            tr = new Triangle(p1, p2, p3);
            DrawTriangle(tr);
        }
        public void DrawQuadrilateralEv(object s, RoutedEventArgs e)
        {
            int w = (int)SliderX.Value;
            int h = (int)SliderY.Value;
            Point2D p = new Point2D(rnd.Next(0, (int)Scene.Width), rnd.Next(0, (int)Scene.Height));
            Quadrilateral qu = new Quadrilateral(p, w, h);
            DrawQuadrilateral(qu);
        }

        public void ClearScene(object sender, RoutedEventArgs e)
        {
            //Очистка Canvas от всех объектов
            Scene.Children.Clear();
        }

        public void AddXT(object s, RoutedEventArgs e)
        {
            tr.addX(10);
        }
    }
}
