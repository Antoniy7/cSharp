using System;

public class BinaryTree<T>
{
    public T value { get; set; }
    public BinaryTree<T> LeftChild { get; set; }
    public BinaryTree<T> RightChild { get; set; }
    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.value = value;
        this.RightChild = rightChild;
        this.LeftChild = leftChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.Write(new string(' ', 2 * indent));
        Console.WriteLine(this.value);
        if (this.LeftChild != null)
        {
            this.LeftChild.PrintIndentedPreOrder(indent + 1);
        }
        if (this.RightChild != null)
        {
            this.RightChild.PrintIndentedPreOrder(indent + 1);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachInOrder(action);
        }
        action(this.value);
        if (this.RightChild != null)
        {
            this.RightChild.EachInOrder(action);
        }

    }

    public void EachPostOrder(Action<T> action)
    {
        if (this.LeftChild != null)
        {
            this.LeftChild.EachPostOrder(action);
        }
        if (this.RightChild != null)
        {
            this.RightChild.EachPostOrder(action);
        }
        action(this.value);
    }
}
