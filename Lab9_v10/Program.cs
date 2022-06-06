
namespace Lab9_v10
{
	delegate void AreaHandler(double area);
	delegate void PointHandler(Point point);

	class Program
	{


		static void Main()
		{
			Rhomb rhomb = new Rhomb();
			rhomb.PointIsWrong += OnPointIsWrong;

			Console.WriteLine("Создание ромба:");
			EditPoints(rhomb);
			Console.WriteLine("1. Изменить координаты точек");
			Console.WriteLine("2. Вычислить площадь");
			Console.WriteLine("3. Выход");
			var p = Console.ReadLine();

			while (p != "3")
			{
				switch (p)
				{
					case "1":
						EditPoints(rhomb);
						break;

					case "2":
						Console.WriteLine($"{rhomb} имеет площадь: {rhomb.Area}\n");
						int count = 4;
						for (int index = 0; index < count; index++)
						{
							Console.WriteLine(rhomb[index]);
						}
						break;

					case "3":
						break;

					default:
						Console.WriteLine("[Ошибка]: Указано неверное действие!");
						PressAnyKey();
						break;

				}
				Console.WriteLine("1. Изменить координаты точек");
				Console.WriteLine("2. Вычислить площадь");
				Console.WriteLine("3. Выход");
			    p = Console.ReadLine();
			}
			rhomb.AreaIsEqualToOne += OnAreaIsEqualToOne;
		}

		// Метод редактирования точек
		public static void EditPoints(Rhomb rhomb)
		{
			while (true)
			{
				try
				{
					int count = 4;
					char[] symbols = new char[count];
					Console.WriteLine("Заполните данные по 4 точкам:");
					for (int index = 0; index < count; index++)
					{
						CheckName(out char name, index + 1, ref symbols);
						CheckCoordinate(out double x, 'X');
						CheckCoordinate(out double y, 'Y');
						rhomb[index] = new Point(name, x, y);
					}
					rhomb = new Rhomb(rhomb[0], rhomb[1], rhomb[2], rhomb[3]);
					Console.WriteLine("\nВаши координаты успешно добавлены в фигуру");
					break;
				}
				catch (ExceptionRhomb exception)
				{
					Console.WriteLine(exception.Message);
				}
			}
		}

		// Метод проверки значения координаты
		public static void CheckName(out char name, int number, ref char[] symbols)
		{
			while (true)
			{
				try
				{
					bool check = false;
					Console.Write($"\nНазвание точки {number}: ");
					name = char.Parse(Console.ReadLine() ?? throw new FormatException());
					foreach (char symbol in symbols)
					{
						if (name == symbol)
						{
							check = true;
							break;
						}
					}
					if (check)
						Console.WriteLine("\n[Ошибка]: Данное название уже используется!");
					else
					{
						symbols[number - 1] = name;
						break;
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("\n[Ошибка]: Необходимо ввести один символ!");
				}
			}
		}

		// Метод проверки значения координаты
		public static void CheckCoordinate(out double value, char coordinate)
		{
			while (true)
			{
				try
				{
					Console.Write($"Координата {coordinate}: ");
					value = double.Parse(Console.ReadLine() ?? throw new FormatException());
					break;
				}
				catch (FormatException)
				{
					Console.WriteLine("\n[Ошибка]: Необходимо ввести число!\n");
				}
			}
		}

		// Обработчик события AreaIsEqualToOne
		public static void OnAreaIsEqualToOne(double area)
		{
			Console.WriteLine("\nНовое значение площади ромба: 1");
			Console.WriteLine($"Предыдущее значение площади ромба: {area}");
		}

		// Обработчик события PointIsWrong
		public static void OnPointIsWrong(Point point)
		{
			throw new ExceptionRhomb($"\n[Ошибка]: Из данных координат точки {point.Name} невозможно построить ромб!\n");
		}

		// Метод очистки консоли после завершения действия
		public static void PressAnyKey()
		{
			Console.WriteLine("\n[Нажмите на любую клавишу, чтобы продолжить]\n");
			Console.ReadKey();
			Console.Clear();
		}
	}
}