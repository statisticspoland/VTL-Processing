using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StatisticsPoland.VtlProcessing.Cli
{
    class TranslateOptions
    {
        public TranslateOptions(
            FileInfo input, 
            FileInfo output, 
            string target, 
            string model, 
            string namespaceMapping, 
            string defaultNamespace,
            bool verbose,
            bool console)
        {
            this.Input = input;
            this.Output = output;
            this.Target = target;
            this.Model = model;
            this.NamespaceMapping = namespaceMapping;
            this.DefaultNamespace = defaultNamespace;
            this.Verbose = verbose;
            this.Console = console;
        }

        public FileInfo Input { get; private set; }
        public FileInfo Output { get; private set; }
        public string Target { get; private set; }
        public string Model { get; private set; }
        public string DefaultNamespace { get; private set; }
        public string NamespaceMapping { get; private set; }
        public bool Verbose { get; private set; }
        public bool Console { get; private set; }
    }
}
