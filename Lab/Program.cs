public struct MyStruct
{
    public int Value;
}

public class MyClass
{
    public int Value;
}

class Program
{
    static void Main()
    {
        // 1. 구조체 (값 형식)
        MyStruct s1 = new MyStruct { Value = 10 };
        MyStruct s2 = s1;   // 값 복사
        s2.Value = 20;

        Console.WriteLine($"s1.Value = {s1.Value}"); // 10
        Console.WriteLine($"s2.Value = {s2.Value}"); // 20

        // 2. 클래스 (참조 형식)
        MyClass c1 = new MyClass { Value = 10 };
        MyClass c2 = c1;    // 참조 복사
        c2.Value = 20;

        Console.WriteLine($"c1.Value = {c1.Value}"); // 20
        Console.WriteLine($"c2.Value = {c2.Value}"); // 20
    }
}