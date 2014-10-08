using LinFu.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LinFu.DynamicProxy.Samples
{
    public class TestInterceptor : IInvokeWrapper
    {
        private readonly object _target;
        public TestInterceptor(object target)
        {
            _target = target;
        }

        #region IInvokeWrapper Members
        public void AfterInvoke(InvocationInfo info, object returnValue)
        {

        }

        public void BeforeInvoke(InvocationInfo info)
        {

        }

        public object DoInvoke(InvocationInfo info)
        {
            // Change the default behavior
            Console.WriteLine("Hello, CodeProject!");
            
            return null;
        }

        #endregion
    }
    public class TestClass
    {
        public virtual void DoSomething()
        {
            Console.WriteLine("Hello, World!");
        }
    }
    class Program
    {
        static void Main()
        {
            TestInterceptor interceptor = new TestInterceptor(new TestClass());
            ProxyFactory factory = new ProxyFactory();

            TestClass test = factory.CreateProxy<TestClass>(interceptor);

            // Turn it back on and replace
            // the method call with the one
            // in the interceptor
            Console.Write("Interception On: ");
            test.DoSomething();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
