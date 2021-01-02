using System;
using System.Text;

namespace Lr3
{
    public partial class BoolVector
    {
        // константные поля: 0/1
        public const bool Zero = false;
        public const bool One = true;
        // массив для хранения значений
        private bool[] values;
        // статическое поле
        private static int count;
        // поле - идентификатор
        private int id;
        // статический конструктор
        static BoolVector()
        {
            count = 0;
        }
        // конструктор без параметров
        public BoolVector():this(0)
        {

        }
        // конструктор с параметром - массив значений
        public BoolVector(bool[] booleans):this(booleans.Length)
        {
            // копируем значения из массива в поле
            Array.Copy(booleans, values, values.Length);
        }
        // конструктор с параметрами: количество элементов (длина вектора)
        // и параметром с указанным значением по умолчанию
        // false говорящее о том что инициализировать все поля
        // 1-ами не требуется
        public BoolVector(int count, bool isAllOnes = false)
        {
            values = new bool[count];
            // идентификатором будет выступать количество
            // созданных раннее экземпляров класса
            Id = BoolVector.count;
            // инкрементируем счетчик
            BoolVector.count++;
            // если требуется заполнить все значениями 1-ами
            if (isAllOnes)
            {
                // то выполняем это действие в цикле
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = One;
                }
            }
        }

        // приватный конструктор принимающий символьный массив
        // в качестве параметра
        private BoolVector(char[] arr) : this(arr.Length)
        {
            for (int i = 0; i < values.Length; i++)
            {
                // true только если значение равно '1'
                values[i] = arr[i] == '1';
            }
        }

        // свойство: длина вектора
        public int Length { get { return values.Length; } }

        // свойство Id:
        public int Id
        {
            get { return id; }
            // доступ на чтения ограничен (возможен только внутри класса)
            private set
            {
                id = value;
            }
        }

        // индексатор
        public bool this[int i]
        {
            get => values[i];
            // сеттер только для чтения чтобы
            // значения в списке нельзя было изменять извне
            private set => values[i] = value;
        }

        // статический метод для вывода информации об объекте
        public static void PrintInfo(BoolVector boolVector)
        {
            Console.WriteLine("Count = {0}; ID = {1}; Vector = {2}", count, boolVector.id, boolVector);
        }

        // переопределение ToString
        public override string ToString()
        {
            var sb = new StringBuilder(values.Length + 2);

            sb.Append('{');
            foreach(var v in values)
            {
                // записываем '1' или '0'
                // в зависимости от значения
                sb.Append(v ? '1' : '0');
            }
            sb.Append('}');

            return sb.ToString();
        }

        // метод для проверки на равенство
        private static bool IsEqual(BoolVector a, BoolVector b)
        {
            // если оба значения - null значит равны
            if (a is null && b is null)
            {
                return true;
            }

            // если один null а второй - нет значит не равны
            if ((a is null && !(b is null))
              || (!(a is null) && b is null))
            {
                return false;
            }
            // если длины векторов отличаются
            // значит не равны
            if (a.Length != b.Length)
            {
                return false;
            }

            // проверяем попарно на равенство
            for (int i = 0; i < a.Length; i++)
            {
                // как только нашли отличающиеся элементы
                if (a[i] != b[i])
                {
                    // значит векора не равны
                    return false;
                }
            }
            // все элементы совпадают
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is BoolVector bv && IsEqual(this, bv);
        }

        public override int GetHashCode()
        {
            // стандартное переопределение
            return HashCode.Combine(values, id, Length);
        }

        // метод для преобразования из строки 
        // вернет true если трансформация успешна
        // вернет false если трансформация неудачна
        public static bool TryParse(string str, out BoolVector boolVector)
        {
            boolVector = null;
            // если строка пустая дальнейшая проверка не нужна
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            // просматриваем строку в цикле посимвольно
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                // если нашли символ отличный от '0' или '1'
                if (ch != '0' && ch != '1')
                {
                    // то строка не является валидной
                    return false;
                }
            }
            // строка состоит только из '0' и '1' - вызываем приватный конструктор
            boolVector = new BoolVector(str.ToCharArray());
            return true;
        }

        // метод который "поглощает" строку
        // выбирая из нее части подходящие для парсинга и возвращает
        // результат в виде вектора
        public static void Consume(ref string str, out BoolVector boolVector)
        {
            boolVector = null;
            // аккумулятор
            var sb = new StringBuilder();
            foreach(var ch in str)
            {
                // если встретился неподходящий символ
                if (ch != '1' && ch != '0')
                {
                    // выходим из цикла
                    break;
                }
                // иначе запоминаем его
                sb.Append(ch);
            }
            // удаляем поглощенные символы
            str = str.Substring(sb.Length);
            // создаем объект
            boolVector = new BoolVector(sb.ToString().ToCharArray());
        }
    }
}
