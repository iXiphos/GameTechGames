using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T>
{
    //Array of Itmes
    T[] items;
    int currentItemCount;

    int childIndexLeft; //Left index of tree
    int childIndexRight; //Right index of tree
    int swapIndex;

    //Heap Object
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }

    //Add to object to heap list
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    //Sort the Heap up
    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        //While unsorted, sort it
        while (true)
        {
            T parentItem = items[parentIndex];
            if(item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    //Remove the first index of heap
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    //Get the size of heap list
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }

    //Modifty item
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    //check if item is in heap list
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    //Sort the heap list down
    void SortDown(T item)
    {
        //While Unsorted, sort it
        while (true)
        {
            childIndexLeft = item.HeapIndex * 2 + 1;
            childIndexRight = item.HeapIndex * 2 + 2;
            swapIndex = 0;
            if (childIndexLeft < currentItemCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentItemCount)
                {
                    if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else return;
        }
    }

    //Swap the two items in a heap list
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int temp = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = temp;
    }

}

//Interface for Heap Item
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }

}
