using System;
using System.Collections.Generic;
class Heap<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    public int Count => heap.Count;

    public void Enqueue(T item)
    {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
    }

    public T Dequeue()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Heap vazia");

        T min = heap[0];
        heap[0] = heap[^1]; // Move o último para o topo
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);

        return min;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[index].CompareTo(heap[parent]) >= 0) break;
            (heap[parent], heap[index]) = (heap[index], heap[parent]);
            index = parent;
        }
    }

    private void HeapifyDown(int index)
    {
        int leftChild, rightChild, smallest;
        while ((leftChild = 2 * index + 1) < heap.Count)
        {
            rightChild = leftChild + 1;
            smallest = (rightChild < heap.Count && heap[rightChild].CompareTo(heap[leftChild]) < 0) ? rightChild : leftChild;
            if (heap[index].CompareTo(heap[smallest]) <= 0) break;
            (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
            index = smallest;
        }
    }
}