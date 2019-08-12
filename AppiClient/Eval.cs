using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
using CitizenFX.Core;

namespace Client
{
    public class Eval
    {
        public static object Run(string expression)
        { 
            // компилятор кода C#
            ICodeCompiler cs = new CSharpCodeProvider().CreateCompiler();
            
            // параметры компиляции: DLL в памяти
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("system.dll");
            cp.GenerateExecutable = false; // создать DLL
            cp.GenerateInMemory = true;  // создать в памяти
        
            // текст программы на C#:
            string code = string.Empty;
            code+="using System;";
            code+="{ public class Evaluate";
            code+="  { public  object GetResult() { return (\r\n" + expression + "\r\n); }";
            code+="    private double sin(double x) { return (Math.Sin(x)); }";
            code+="    private double cos(double x) { return (Math.Cos(x)); }";
            code+="  }";
            code+="}";
    
            // компиляция исходного кода и получение сборки
            CompilerResults cr=cs.CompileAssemblyFromSource(cp, code);
            if (cr.Errors != null && cr.Errors.Count > 0)
            {
                Debug.WriteLine($"Row {cr.Errors}");
                foreach (object t in cr.Errors)
                    Debug.WriteLine($"Col {t}");
                return null;
            }
    
            try // создать объект и вызвать метод для вычисления выражения
            {
                object ob=cr.CompiledAssembly.CreateInstance("Evaluate");
                if (ob != null)
                    return ob.GetType().InvokeMember("GetResult", BindingFlags.InvokeMethod, null, ob,
                        new object[] { });
            } 
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null;
        }
    }
}