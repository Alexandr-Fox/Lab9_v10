namespace Lab9_v10
{
	class Point
	{
		private static int _count; // Количество введённых точек
		private static int _borderX = 2; // Крайнее положительное значение X (крайнее отрицательное со знаком "минус")
		private static int _borderY = -1; // Крайнее отрицательное значение Y (крайнее положительное со знаком "плюс")
		private readonly char[] _names = { 'A', 'B', 'C', 'D' }; // Массив из названий точек по умолчанию

		// Конструктор без параметров
		public Point()
		{
			Name = _names[_count];
			if (_count % 2 == 0)
			{
				_borderX *= -1; // Смена крайней границы на противоположную
				X = _borderX;
			}
			else
				X = 0;

			if (_count % 2 == 0)
				Y = 0;
			else
			{
				_borderY *= -1; // Смена крайней границы на противоположную
				Y = _borderY;
			}
			_count++;
		}
		// Конструктор с параметрами
		public Point(char name, double x, double y)
		{
			Name = name;
			X = x;
			Y = y;
		}
		public char Name { get; set; } // Свойство Name
		public double X { get; set; } // Свойство X
		public double Y { get; set; } // Свойство Y
		// Переопределение метода ToString
		public override string ToString()
		{
			return string.Format($"Точка {Name}: ({X}; {Y})");
		}
	}
}
