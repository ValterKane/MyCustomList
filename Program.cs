using MyPersonArray;
using System.LINQ;

MyCustomList<char> myList = new MyCustomList<char>();

myList.Add('c');

Console.WriteLine(string.Join(' ', myList));

myList.Clear();

char[] arr = new char[3] { 'a', 'b','c' };

myList.AddRange(arr);

myList.AddRange('d', 'A', 'T');

List<char> list = new List<char>();
list.Add('A');

myList.AddRange(list);

Console.WriteLine(string.Join(' ', myList));

Console.WriteLine($"Contains A is {myList.Contains('A')}");

myList.RemoveAll('A');

myList.ElementAt(); // <- пренадлежит LINQ

Console.WriteLine($"Contains A is {myList.Contains('A')}");

foreach(var item in myList)
{

    Console.WriteLine($"{item}")
}
