using System;
using System.Collections.Generic;

public class Tree<T>
{
       public T value {get; set;}
      public IList<Tree<T>> Childer{get; private set;}
     public Tree(T value, params Tree<T>[] children)
     {
         this.value=value;
         this.Childer=new List <Tree<T>>();
         foreach(var child in children)
         {
             this.Childer.Add(child);
         }

     }
     
      
    public void Print(int indent = 0)
    {
        Console.Write(new string(' ',2*indent));
        Console.WriteLine(this.value);
        foreach(var child in this.Childer)
        {
            child.Print(indent+1);
        }  
    }

    public void Each(Action<T> action)
    {
        action(this.value);
        foreach(var child in this.Childer)
        {
            child.Each(action);
        }
    }

}

