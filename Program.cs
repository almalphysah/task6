using System;

class Program
{
    static void Main()
    {
        // 1. Создать целочисленную матрицу температур int[,] cpuTemps размером 6x5 (значения от 35 до 95 градусов).
        int[,] cpuTemps =
        {
            { 55, 68, 72, 61, 48 },
            { 75, 83, 66, 59, 70 },
            { 45, 91, 78, 64, 52 },
            { 69, 74, 88, 57, 63 },
            { 81, 62, 71, 94, 56 },
            { 53, 67, 79, 85, 60 }
        };

        // 2. Создать параллельную символьную матрицу статусов char[,] rackStatus.
        char[,] rackStatus = new char[6, 5];

        // 3. Заполнить статусы по правилу: > 80 градусов — !, от 65 до 80 — W, < 65 — ..
        for (int i = 0; i < cpuTemps.GetLength(0); i++)
        {
            for (int j = 0; j < cpuTemps.GetLength(1); j++)
            {
                if (cpuTemps[i, j] > 80)
                {
                    rackStatus[i, j] = '!';
                }
                else if (cpuTemps[i, j] >= 65)
                {
                    rackStatus[i, j] = 'W';
                }
                else
                {
                    rackStatus[i, j] = '.';
                }
            }
        }

        // 4. Разместить центральный блок охлаждения C в ячейках [0, 2] и [5, 2].
        rackStatus[0, 2] = 'C';
        rackStatus[5, 2] = 'C';

        // 5. Вывести тепловую карту в консоль с форматированием температур.
        Console.WriteLine("Тепловая карта:");
        Console.WriteLine();

        for (int i = 0; i < cpuTemps.GetLength(0); i++)
        {
            for (int j = 0; j < cpuTemps.GetLength(1); j++)
            {
                Console.Write($"{cpuTemps[i, j],3} {rackStatus[i, j]} ");
            }

            Console.WriteLine();
        }

        // 6. Найти самый горячий процессор стойки и вывести его температуру и координаты.
        int maxTemp = cpuTemps[0, 0];
        int maxRow = 0;
        int maxCol = 0;

        for (int i = 0; i < cpuTemps.GetLength(0); i++)
        {
            for (int j = 0; j < cpuTemps.GetLength(1); j++)
            {
                if (cpuTemps[i, j] > maxTemp)
                {
                    maxTemp = cpuTemps[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Самый горячий процессор: {maxTemp} градусов");
        Console.WriteLine($"Координаты: [{maxRow + 1}, {maxCol + 1}]");

        // 7. Подсчитать среднюю температуру каждого ряда серверов (по строкам).
        Console.WriteLine();
        Console.WriteLine("Средняя температура каждого ряда:");

        for (int i = 0; i < cpuTemps.GetLength(0); i++)
        {
            int rowSum = 0;

            for (int j = 0; j < cpuTemps.GetLength(1); j++)
            {
                rowSum += cpuTemps[i, j];
            }

            double average = (double)rowSum / cpuTemps.GetLength(1);

            Console.WriteLine($"Ряд {i}: {average:F2} градусов");
        }

        // 8. Определить общее число процессоров в критическом состоянии !.
        int criticalCount = 0;

        for (int i = 0; i < rackStatus.GetLength(0); i++)
        {
            for (int j = 0; j < rackStatus.GetLength(1); j++)
            {
                if (rackStatus[i, j] == '!')
                {
                    criticalCount++;
                }
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Кол-во процессоров в критическом состоянии: {criticalCount}");

        // 9. Запросить у системного администратора координаты аварийного юнита для принудительного отключения (записать 0 градусов).
        Console.WriteLine();
        Console.Write("Введите строку: ");
        int row = int.Parse(Console.ReadLine());

        Console.Write("Введите столбец: ");
        int col = int.Parse(Console.ReadLine());

        if (row >= 0 && row < cpuTemps.GetLength(0) &&col >= 0 && col < cpuTemps.GetLength(1))
        {
            cpuTemps[row, col] = 0;

            Console.WriteLine($"Аварийный юнит [{row}, {col}] отключен.");
        }
        else
        {
            Console.WriteLine("Ошибка: координаты вне матрицы.");
        }

        // 10. Проверить главную диагональ: если средняя температура на ней выше 70 градусов, вывести сигнал общей тревоги.
        int diagonalSum = 0;

        int diagonalCount = Math.Min(cpuTemps.GetLength(0), cpuTemps.GetLength(1));

        for (int i = 0; i < diagonalCount; i++)
        {
            diagonalSum += cpuTemps[i, i];
        }

        double diagonalAverage = (double)diagonalSum / diagonalCount;

        Console.WriteLine();
        Console.WriteLine($"Средняя температура главной диагонали: {diagonalAverage:F2}");

        if (diagonalAverage > 70)
        {
            Console.WriteLine("Сигнал общей тревоги! Средняя температура выше 70 градусов!");
        }
        else
        {
            Console.WriteLine("Температура в норме.");
        }
    }
}