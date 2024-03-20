using MyPersonArray;

MyCustomList<char> myList = new MyCustomList<char>();

myList.Add('c');

Console.WriteLine(string.Join(' ', myList));

myList.Clear();

myList.Add('A');

Console.WriteLine(string.Join(' ', myList));
