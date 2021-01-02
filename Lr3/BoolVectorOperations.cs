using System;

namespace Lr3
{
    // продолжение класса
    public partial class BoolVector
    {
        // коньюнкция (логическое И)
        public void And(BoolVector boolVector)
        {
            // находим длину
            var lng = Math.Max(Length, boolVector.Length);
            // если длины текущего недостаточно
            if (Length < lng)
            {
                // расширяем его
                Array.Resize(ref values, lng);
            }
            // выполняем преобразование
            for (int i = 0; i < lng; i++)
            {
                // true если и то и другое true
                values[i] = values[i] && boolVector[i];
            }
        }

        // дизъюнкция (логическое ИЛИ)
        public void Or(BoolVector boolVector)
        {
            // находим длину
            var lng = Math.Max(Length, boolVector.Length);
            // если длины текущего недостаточно
            if (Length < lng)
            {
                Array.Resize(ref values, lng);
            }
            // выполняем преобразование
            for (int i = 0; i < lng; i++)
            {
                // true если хотя-бы одно true
                values[i] = values[i] || boolVector[i];
            }
        }

        // отрицание
        public void Negate()
        {
            for (int i = 0; i < Length; i++)
            {
                // берем отрицание (обратное значение)
                values[i] = !values[i];
            }
        }

        // метод для подсчета нулей
        public int CountZeros()
        {
            // количество нулей
            var count = 0;
            // перебираем все элементы в цикле
            for (int i = 0; i < values.Length; i++)
            {
                // если является 0-ем
                if (!values[i])
                {
                    // увеличиваем счетчик
                    count++;
                }
            }

            return count;
        }

        // метод для подсчета единиц
        public int CountOnes()
        {
            // количество единиц
            var count = 0;
            // перебираем все элементы в цикле
            for (int i = 0; i < values.Length; i++)
            {
                // если является 1-ей
                if (values[i])
                {
                    // увеличиваем счетчик
                    count++;
                }
            }

            return count;
        }
    }
}
