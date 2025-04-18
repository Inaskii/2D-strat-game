using System.Collections.Generic;

public class BinaryHeap<T>
{
    private List<(T item, float priority)> heap = new List<(T, float)>();

    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add((item, priority));
        int currentIndex = heap.Count - 1;

        while (currentIndex > 0)
        {
            int parentIndex = (currentIndex - 1) / 2;
            if (heap[currentIndex].priority >= heap[parentIndex].priority)
                break;

            (heap[currentIndex], heap[parentIndex]) = (heap[parentIndex], heap[currentIndex]);
            currentIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        if (heap.Count == 0) return default;

        T result = heap[0].item;
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        int currentIndex = 0;
        while (true)
        {
            int leftChild = currentIndex * 2 + 1;
            int rightChild = currentIndex * 2 + 2;
            int smallest = currentIndex;

            if (leftChild < heap.Count && heap[leftChild].priority < heap[smallest].priority)
                smallest = leftChild;
            if (rightChild < heap.Count && heap[rightChild].priority < heap[smallest].priority)
                smallest = rightChild;

            if (smallest == currentIndex)
                break;

            (heap[currentIndex], heap[smallest]) = (heap[smallest], heap[currentIndex]);
            currentIndex = smallest;
        }

        return result;
    }

    public void Clear()
    {
        heap.Clear();
    }
}