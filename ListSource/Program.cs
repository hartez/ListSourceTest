using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSource
{
    class Program
    {
        static void Main(string[] args)
        {
            var testSource0 = new TestSource();
            var testSource1 = new TestSource();
            var testSource2 = new TestSource();

            var watch = new Stopwatch();
           
            watch.Start();
            var ls1 = new ListSource1(testSource1);
            watch.Stop();
            Console.WriteLine($">>>>> Program Main ListSource1, count is {ls1.Count}: {watch.ElapsedMilliseconds}");
            watch.Reset();

            watch.Start();
            var ls2 = new ListSource2(testSource2);
            watch.Stop();
            Console.WriteLine($">>>>> Program Main ListSource2, count is {ls2.Count}: {watch.ElapsedMilliseconds}");
            watch.Reset();

            watch.Start();
            var ls0 = new ListSource0(testSource0);
            watch.Stop();
            Console.WriteLine($">>>>> Program Main ListSource0, count is {ls0.Count}: {watch.ElapsedMilliseconds}");
            watch.Reset();

            Console.ReadLine();
        }
    }

    internal class TestSource : IEnumerable, IEnumerator
    {
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        private int _currentIndex;
        private int _max = 10000000;
        
        public bool MoveNext()
        {
            _currentIndex = _currentIndex + 1;
            return _currentIndex != _max;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public object Current => _currentIndex;
    }

    internal class ListSource0 : List<object>
    {
        public ListSource0(IEnumerable enumerable) 
        {
            foreach (object item in enumerable)
            {
                Add(item);
            }
        }
    }

    internal class ListSource1 : List<object>
    {
        public ListSource1(IEnumerable enumerable) 
        {
            InsertRange(0, enumerable.Cast<object>());
        }
    }

    internal class ListSource2 : List<object>
    {
        public ListSource2(IEnumerable enumerable) : base(enumerable.Cast<object>())
        {
           
        }
    }
}
