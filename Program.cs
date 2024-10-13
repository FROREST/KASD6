using System;
using System.Collections.Generic;
namespace System
{
public class MyVector<T>
{
    private T[] elementData; 
    private int elementCount; 
    private int capacityIncrement; 
    public MyVector(int initialCapacity, int capacityIncrement)
    {
        if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));
        elementData = new T[initialCapacity];
        this.capacityIncrement = capacityIncrement;
        elementCount = 0;
    }

   
    public MyVector(int initialCapacity) : this(initialCapacity, 0) { }

    // Конструктор по умолчанию с ёмкостью 10 и приращением по умолчанию (0)
    public MyVector() : this(10) { }

    // Конструктор, заполняющий вектор элементами из массива
    public MyVector(T[] a)
    {
        elementData = new T[a.Length];
        Array.Copy(a, elementData, a.Length);
        elementCount = a.Length;
        capacityIncrement = 0;
    }

    // Метод для увеличения ёмкости
    private void EnsureCapacity(int minCapacity)
    {
        if (minCapacity > elementData.Length)
        {
            int newCapacity = elementData.Length;
            if (capacityIncrement > 0)
                newCapacity += capacityIncrement;
            else
                newCapacity = elementData.Length * 2;

            if (newCapacity < minCapacity) newCapacity = minCapacity;

            T[] newArray = new T[newCapacity];
            Array.Copy(elementData, newArray, elementCount);
            elementData = newArray;
        }
    }

    // Метод добавления элемента в конец
    public void Add(T e)
    {
        EnsureCapacity(elementCount + 1);
        elementData[elementCount++] = e;
    }

    // Метод для добавления всех элементов из массива
    public void AddAll(T[] a)
    {
        EnsureCapacity(elementCount + a.Length);
        Array.Copy(a, 0, elementData, elementCount, a.Length);
        elementCount += a.Length;
    }

    // Метод очистки вектора
    public void Clear()
    {
        elementCount = 0;
    }

    // Метод для проверки, содержится ли элемент в векторе
    public bool Contains(T o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i].Equals(o)) return true;
        }
        return false;
    }

    // Метод для проверки, содержатся ли все элементы массива в векторе
    public bool ContainsAll(T[] a)
    {
        foreach (T item in a)
        {
            if (!Contains(item)) return false;
        }
        return true;
    }

    // Метод для проверки, пуст ли вектор
    public bool IsEmpty()
    {
        return elementCount == 0;
    }

    // Метод удаления элемента
    public bool Remove(T o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i].Equals(o))
            {
                RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    // Метод для удаления всех элементов из массива
    public void RemoveAll(T[] a)
    {
        foreach (T item in a)
        {
            Remove(item);
        }
    }

    // Метод для замены элемента
    public T Set(int index, T element)
    {
        if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
        T oldValue = elementData[index];
        elementData[index] = element;
        return oldValue;
    }

    // Метод для получения размера вектора
    public int Size()
    {
        return elementCount;
    }

    // Метод для удаления элемента по индексу
    public T RemoveAt(int index)
    {
        if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
        T oldValue = elementData[index];
        Array.Copy(elementData, index + 1, elementData, index, elementCount - index - 1);
        elementCount--;
        return oldValue;
    }

    // Метод для получения элемента по индексу
    public T Get(int index)
    {
        if (index >= elementCount || index < 0) throw new ArgumentOutOfRangeException(nameof(index));
        return elementData[index];
    }

    // Метод для получения массива всех элементов
    public T[] ToArray()
    {
        T[] result = new T[elementCount];
        Array.Copy(elementData, result, elementCount);
        return result;
    }

    // Метод для возврата подмножества вектора
    public MyVector<T> SubList(int fromIndex, int toIndex)
    {
        if (fromIndex < 0 || toIndex > elementCount || fromIndex > toIndex)
            throw new ArgumentOutOfRangeException();

        T[] subArray = new T[toIndex - fromIndex];
        Array.Copy(elementData, fromIndex, subArray, 0, toIndex - fromIndex);
        return new MyVector<T>(subArray);
    }

    // Метод для нахождения индекса элемента
    public int IndexOf(T o)
    {
        for (int i = 0; i < elementCount; i++)
        {
            if (elementData[i].Equals(o)) return i;
        }
        return -1;
    }

    // Метод для нахождения последнего индекса элемента
    public int LastIndexOf(T o)
    {
        for (int i = elementCount - 1; i >= 0; i--)
        {
            if (elementData[i].Equals(o)) return i;
        }
        return -1;
    }

    // Метод для возвращения первого элемента
    public T FirstElement()
    {
        if (IsEmpty()) throw new InvalidOperationException("Vector is empty");
        return elementData[0];
    }

    // Метод для возвращения последнего элемента
    public T LastElement()
    {
        if (IsEmpty()) throw new InvalidOperationException("Vector is empty");
        return elementData[elementCount - 1];
    }

    // Метод для удаления элемента на заданной позиции
    public void RemoveElementAt(int pos)
    {
        RemoveAt(pos);
    }

    // Метод для удаления нескольких элементов подряд
    public void RemoveRange(int begin, int end)
    {
        if (begin < 0 || end > elementCount || begin > end)
            throw new ArgumentOutOfRangeException();

        int numMoved = elementCount - end;
        Array.Copy(elementData, end, elementData, begin, numMoved);
        elementCount -= (end - begin);
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        MyVector<int> vector = new MyVector<int>();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Add элемент");
            Console.WriteLine("2. AddAll элементы");
            Console.WriteLine("3. Clear");
            Console.WriteLine("4. Contains");
            Console.WriteLine("5. ContainsAll");
            Console.WriteLine("6. IsEmpty");
            Console.WriteLine("7. Remove элемент");
            Console.WriteLine("8. RemoveAll");
            Console.WriteLine("9. Size");
            Console.WriteLine("10. ToArray");
            Console.WriteLine("11. Get элемент по индексу");
            Console.WriteLine("12. IndexOf");
            Console.WriteLine("13. LastIndexOf");
            Console.WriteLine("14. FirstElement");
            Console.WriteLine("15. LastElement");
            Console.WriteLine("16. Remove элемент по индексу");
            Console.WriteLine("17. Remove Range");
            Console.WriteLine("18. EXIT");
            string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Введите элемент для добавления: ");
                int addElement = int.Parse(Console.ReadLine());
                vector.Add(addElement);
                break;

            case "2":
                Console.Write("Введите элементы через запятую: ");
                string[] addElements = Console.ReadLine().Split(',');
                int[] elementsArray = Array.ConvertAll(addElements, int.Parse);
                vector.AddAll(elementsArray);
                break;

            case "3":
                vector.Clear();
                Console.WriteLine("Вектор очищен.");
                break;

            case "4":
                Console.Write("Введите элемент для проверки: ");
                int containsElement = int.Parse(Console.ReadLine());
                Console.WriteLine(vector.Contains(containsElement) ? "Содержится" : "Не содержится");
                break;

            case "5":
                Console.Write("Введите элементы через запятую для проверки ContainsAll: ");
                string[] checkElements = Console.ReadLine().Split(',');
                int[] checkArray = Array.ConvertAll(checkElements, int.Parse);
                Console.WriteLine(vector.ContainsAll(checkArray) ? "Все содержатся" : "Не все содержатся");
                break;

            case "6":
                Console.WriteLine(vector.IsEmpty() ? "Вектор пуст" : "Вектор не пуст");
                break;

            case "7":
                Console.Write("Введите элемент для удаления: ");
                int removeElement = int.Parse(Console.ReadLine());
                vector.Remove(removeElement);
                break;

            case "8":
                Console.Write("Введите элементы через запятую для удаления: ");
                string[] removeElements = Console.ReadLine().Split(',');
                int[] removeArray = Array.ConvertAll(removeElements, int.Parse);
                vector.RemoveAll(removeArray);
                break;

            case "9":
                Console.WriteLine($"Размер вектора: {vector.Size()}");
                break;

            case "10":
                int[] array = vector.ToArray();
                Console.WriteLine($"Элементы вектора: {string.Join(", ", array)}");
                break;

            case "11":
                Console.Write("Введите индекс: ");
                int index = int.Parse(Console.ReadLine());
                Console.WriteLine($"Элемент на индексе {index}: {vector.Get(index)}");
                break;

            case "12":
                Console.Write("Введите элемент для поиска его индекса: ");
                int searchElement = int.Parse(Console.ReadLine());
                Console.WriteLine($"Индекс: {vector.IndexOf(searchElement)}");
                break;

            case "13":
                Console.Write("Введите элемент для поиска последнего индекса: ");
                int lastSearchElement = int.Parse(Console.ReadLine());
                Console.WriteLine($"Последний индекс: {vector.LastIndexOf(lastSearchElement)}");
                break;

            case "14":
                Console.WriteLine($"Первый элемент: {vector.FirstElement()}");
                break;

            case "15":
                Console.WriteLine($"Последний элемент: {vector.LastElement()}");
                break;

            case "16":
                Console.Write("Введите индекс для удаления элемента: ");
                int removeIndex = int.Parse(Console.ReadLine());
                vector.RemoveAt(removeIndex);
                break;

            case "17":
                Console.Write("Введите начальный и конечный индексы для удаления диапазона (через пробел): ");
                string[] range = Console.ReadLine().Split(' ');
                int begin = int.Parse(range[0]);
                int end = int.Parse(range[1]);
                vector.RemoveRange(begin, end);
                break;

            case "18":
                running = false;
                Console.WriteLine("Программа завершена.");
                break;

            default:
                Console.WriteLine("Неверный выбор.");
                break;
            }
        }
    }
}
}