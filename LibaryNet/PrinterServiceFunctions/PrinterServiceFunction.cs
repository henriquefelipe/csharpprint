using LibaryNet.Driver;
using CSharpPrint;
using System.Collections.Generic;
using System.Linq;

namespace LibaryNet.PrinterServiceFunctions
{
    internal abstract class PrinterServiceFunction
    {
        protected PrinterService Service { get; }

        protected PrinterDriverBase PrinterDriver
        {
            get
            {
                return Service.printerdriver;
            }
        }

        private List<FunctionParameter> Parameters { get; set; }

        public PrinterServiceFunction(PrinterService service)
        {
            Service = service;
            Parameters = new List<FunctionParameter>();
        }

        public void AddParameter(string name, object value)
        {
            Parameters.Add(new FunctionParameter(name, value));
        }

        protected FunctionParameter GetParameter(string name)
        {
            return Parameters.FirstOrDefault(p => p.Name.Equals(name));
        }

        public object FunctionResult { get; protected set; }

        protected abstract void Run();

        public void RunFunction()
        {
            Run();
        }
    }
}
