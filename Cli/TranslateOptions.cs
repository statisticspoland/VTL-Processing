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
            FileInfo namespaceMapping, 
            string defaultNamespace)
        {
            this.Input = input;
            this.Output = output;
            this.Target = target;
            this.Model = model;
            this.NamespaceMapping = namespaceMapping;
            this.DefaultNamespace = defaultNamespace;
        }

        public FileInfo Input { get; private set; }
        public FileInfo Output { get; private set; }
        public string Target { get; private set; }
        public string Model { get; private set; }
        public string DefaultNamespace { get; private set; }
        public FileInfo NamespaceMapping { get; private set; }
    }
}
