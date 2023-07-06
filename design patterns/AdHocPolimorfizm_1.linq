namespace AdHocPolimorfizm
{

    class B
    {
        public virtual void f(int x) { Console.WriteLine("B.f(int)"); }
    }

    class D : B
    {
        public override void f(int x) { Console.WriteLine("D.f(int)"); }
    }
    class C : B 
    {
        public new void f(int x) { Console.WriteLine("C.f(int)"); }
    }
    class Program
    {
        static void Main(string[] args)
        {

            D myClass = new D();
            B myClass1 = myClass;

            C myClass2 = new C();
            B MyClass3 = myClass2;
            myClass.f(1);
            myClass1.f(1);
            myClass2.f(1);
            MyClass3.f(1);

        }
    }
}
