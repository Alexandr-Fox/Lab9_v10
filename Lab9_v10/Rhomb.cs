
namespace Lab9_v10
{
    internal class Rhomb
    {
        public event AreaHandler? AreaIsEqualToOne; // Событие AreaIsEqualToOne
        public event PointHandler? PointIsWrong; // Событие PointIsWrong
        private double TOLERANCE = 0.0001;

        private Point[] _points = new Point[4]; // Массив, хранящий 4 точки

        private static double _previousArea; // Значение площади ромба по предыдущим координатам
        private static double _lengthAb; // Расстояние между первой и второй введённой точки
        private static bool _isRhomb = true; // Ограничение на присваивание предыдущей площади

        // Конструктор без параметров
        public Rhomb()
        {
            for (var index = 0; index < 4; index++)
                _points[index] = new Point();
        }

        // Конструктор с параметрами
        public Rhomb(Point first, Point second, Point third, Point fourth)
        {
            _points[0] = first;
            _points[1] = second;
            _points[2] = third;
            _points[3] = fourth;
        }

        // Свойство нахождения площади ромба
        public double Area => Math.Abs(_points[2].Y - _points[0].Y) * Math.Abs(_points[3].X - _points[1].X) / 2;

        // Индексатор
        public Point this[int q]
        {
            get => _points[q];
            set
            {
                if (q == 0 && _isRhomb)
                    _previousArea = Area; // Предыдущая площадь

                _points[q] = value;

                if (q == 1)
                {
                    _lengthAb = Math.Sqrt(Math.Pow(_points[q - 1].X - _points[q].X, 2) + Math.Pow(_points[q - 1].Y - _points[q].Y, 2));
                    if (_lengthAb == 0)
                    {
                        _isRhomb = false;
                        PointIsWrong?.Invoke(_points[q]);
                    }
                }

                if (q > 1)
                {
                    double lengthBc = Math.Sqrt(Math.Pow(_points[q - 1].X - _points[q].X, 2) + Math.Pow(_points[q - 1].Y - _points[q].Y, 2));
                    double lengthAc = Math.Sqrt(Math.Pow(_points[q - 2].X - _points[q].X, 2) + Math.Pow(_points[q - 2].Y - _points[q].Y, 2));
                    if (Math.Abs(_lengthAb - lengthBc) > TOLERANCE || Math.Abs(lengthAc - (2 * _lengthAb)) < TOLERANCE || lengthAc == 0)
                    {
                        _isRhomb = false;
                        PointIsWrong?.Invoke(_points[q]);
                    }
                }

                if (q == 3 && Math.Abs(Area - 1) < TOLERANCE && _previousArea != 0) // Текущая площадь (после последовательного ввода 4 точек)
                    AreaIsEqualToOne?.Invoke(_previousArea);

                if (!_isRhomb)
                    _isRhomb = true;
            }
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return string.Format($"Ромб {_points[0].Name}{_points[1].Name}{_points[2].Name}{_points[3].Name}");
        }
    }
}
