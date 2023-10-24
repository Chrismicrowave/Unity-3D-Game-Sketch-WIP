using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public ItemData data;
    public int stackSize;

    public Item(ItemData source)
    {
        data = source;
        AddToStack();
    }

    public void AddToStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }

}
