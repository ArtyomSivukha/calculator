namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        SafeCalculate();

        bool continueProgram = true;
        while (continueProgram)
        {
            Console.WriteLine("1. Выполнить новую оперпацию");
            Console.WriteLine("2. Выйти из программы");
            Console.Write("Выберите: ");

            string menuChoice = Console.ReadLine();
            switch (menuChoice)
            {
                case "1":
                    SafeCalculate();
                    break;
                case "2":
                    continueProgram = false;
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Введите цифру 1 или 2");
                    break;
            }
        }
    }

    private static void SafeCalculate()
    {
        try
        {
            Calculate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void Calculate()
    {
        var firstNumber = ReadNumber("Введите певрое число: ");

        var operation = ReadOperation();

        var secondNumber = ReadNumber("Введите второе число: ");

        double result = operation switch
        {
            Operation.Addition => firstNumber + secondNumber,
            Operation.Subtraction => firstNumber - secondNumber,
            Operation.Multiplication => firstNumber * secondNumber,
            Operation.Division when secondNumber == 0 => throw new DivideByZeroException(
                "Ошибка: Деление на ноль невозможно!"),
            Operation.Division => firstNumber / secondNumber
        };
        Console.WriteLine($"Результат вычисления равен: {result}");
    }

    private static double ReadNumber(string inputMessage)
    {
        Console.Write(inputMessage);
        double number;
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Ошибка: введите корректное число!");
            Console.Write(inputMessage);
        }

        return number;
    }

    private static Operation ReadOperation()
    {
        var operationsMapping = new Dictionary<char, Operation>()
        {
            { '+', Operation.Addition },
            { '-', Operation.Subtraction },
            { '*', Operation.Multiplication },
            { '/', Operation.Division }
        };

        Console.WriteLine("Введите номер арифметической опреации (1-4):");
        foreach (var pair in operationsMapping)
        {
            Console.WriteLine($"{pair.Value:D}: {pair.Key}");
        }

        Operation operation;
        while (!Enum.TryParse(Console.ReadLine(), out operation))
        {
            Console.WriteLine("Введите число от 1 до 4");
        }

        return operation;
    }
}