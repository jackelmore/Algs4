using System.Collections;
using System.Collections.Generic;

namespace jackel.Collections
{
    public class ArrayListStack<T> : IEnumerable<T>
    {
        private T[] array;
        private int N = 0;

        public ArrayListStack()
        {
            array = new T[1];
        }
        public ArrayListStack(int initialCapacity)
        {
            array = new T[initialCapacity];
        }
        public bool isEmpty()
        {
            return N == 0;
        }
        public T pop()
        {
            T item = array[--N];
            array[N] = default(T);
            if (N > 0 && N == array.Length / 4)
                resize(array.Length / 2);
            return item;
        }
        public void push(T item)
        {
            if (N == array.Length)
                resize(2 * array.Length);
            array[N++] = item;
        }
        private void resize(int capacity)
        {
            T[] copy = new T[capacity];
            for (int i = 0; i < N; i++)
                copy[i] = array[i];
            array = copy;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int count = 0; count < N; count++)
                yield return array[count];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } 
    public class LinkedListStack<T> : IEnumerable<T>
    {
        private Node first = null;
        class Node
        {
            public T item;
            public Node next;
        }

        public bool isEmpty()
        {
            return first == null;
        }
        public void push(T item)
        {
            Node oldFirst = first;
            first = new Node();
            first.item = item;
            first.next = oldFirst;
        }
        public T pop()
        {
            T item = first.item;
            first = first.next;
            return item;
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node current = first;
            while(current != null)
            {
                yield return current.item;
                current = current.next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    }
    class LinkedListQueue<T> : IEnumerable<T>
    {
        private Node first, last;

        class Node
        {
            public T item;
            public Node next;
        }
        public bool isEmpty()
        {
            return first == null;
        }
        public void enqueue(T item)
        {
            Node oldlast = last;
            last = new Node();
            last.item = item;
            last.next = null;
            if (isEmpty())
                first = last;
            else
                oldlast.next = last;
        }
        public T dequeue()
        {
            T item = first.item;
            first = first.next;
            if (isEmpty())
                last = null;
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = first;
            while (current != null)
            {
                yield return current.item;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
