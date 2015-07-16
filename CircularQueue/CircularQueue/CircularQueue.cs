using System;

public class CircularQueue<T>
{
    private T[] elements;
    private int startIndex = 0;
    private int endIndex = 0;
    private const int InitialCapacity = 16;


    private void Grow()
    {
        //throw new NotImplementedException();
        var newElements = new T[2 * this.elements.Length];
        this.CopyAllElements(newElements);
        this.elements = newElements;
        startIndex = 0;
        endIndex = this.Count;
    }
    private void CopyAllElements(T[] resultArr)
    {

        int sourceIndex = this.startIndex;
        int destitaniotIndex = 0;
        for (int i = 0; i <this.Count; i++)
        {
            resultArr[destitaniotIndex] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length;
            destitaniotIndex++;
        }
       
    }


    public int Count { get; private set; }

    //public CircularQueue()
    //{
    //   // Count = 0;
    //}

    public CircularQueue(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
        //this.Count = capacity;
    }

    public void Enqueue(T element)
    {
       if (this.Count>= this.elements.Length)
       {
           this.Grow();
       }
       this.elements[this.endIndex] = element;
       this.endIndex = (this.endIndex + 1) % this.elements.Length;
       this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count==0)
        {
            throw new InvalidOperationException("the queue is empty! ");
        }


        T result = this.elements[startIndex];
        startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;
        return result;

    }

    public T[] ToArray()
    {
        var resultArr = new T[this.Count];
        CopyAllElements(resultArr);

         return resultArr;
       
       // throw new NotImplementedException();
    }
}


class Example
{
    static void Main()
    {
        var queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        var first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
