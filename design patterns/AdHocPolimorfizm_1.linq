<Query Kind="Statements">
  <Output>DataGrids</Output>
  <NuGetReference>App.Metrics.AspNetCore</NuGetReference>
  <NuGetReference>App.Metrics.AspNetCore.Core</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Http.Features</NuGetReference>
  <NuGetReference>Microsoft.AspNetCore.Mvc</NuGetReference>
  <NuGetReference>Microsoft.EntityFrameworkCore</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Configuration</NuGetReference>
  <NuGetReference>Microsoft.Extensions.DependencyInjection</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Http.Polly</NuGetReference>
  <NuGetReference>NUnitLite</NuGetReference>
  <NuGetReference>System.Threading.Tasks.Dataflow</NuGetReference>
  <Namespace>Microsoft.AspNetCore.Builder</Namespace>
  <Namespace>Microsoft.Extensions.Hosting</Namespace>
  <Namespace>Microsoft.Extensions.Logging</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
