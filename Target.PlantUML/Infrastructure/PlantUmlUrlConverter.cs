namespace StatisticsPoland.VtlProcessing.Target.PlantUML.Infrastructure
{
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;

    public class PlantUmlUrlConverter
    {
        private string source;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantUmlUrlConverter"/> class.
        /// </summary>
        /// <param name="apiAddress">The PlantUML server address.</param>
        /// <param name="source">The PlantUML source code.</param>
        public PlantUmlUrlConverter(string source = null, string apiAddress = "http://www.plantuml.com/plantuml")
        {
            if (apiAddress.Last() != '/') this.ApiAddress = apiAddress;
            else this.ApiAddress = apiAddress.Remove(apiAddress.Length - 1);

            this.Source = source;
        }

        /// <summary>
        /// Gets the URL of a PlantUML Server.
        /// </summary>
        public string ApiAddress { get; }

        /// <summary>
        /// Gets or sets the PlantUML source code to process.
        /// </summary>
        public string Source
        {
            get => this.source;
            set
            {
                this.source = value;
                string compressedSource = this.source == null ? this.source : this.CompressSourceToPlantUmlUrl();

                this.SVGUrl = $"{this.ApiAddress}/svg/{compressedSource}";
                this.PNGUrl = $"{this.ApiAddress}/png/{compressedSource}";
                this.TXTUrl = $"{this.ApiAddress}/txt/{compressedSource}";
            }
        }

        /// <summary>
        /// Gets the URL of a diagram (SVG).
        /// </summary>
        public string SVGUrl { get; private set; }

        /// <summary>
        /// Gets the URL of a diagram (PNG).
        /// </summary>
        public string PNGUrl { get; private set; }

        /// <summary>
        /// Gets the URL of a diagram (TXT).
        /// </summary>
        public string TXTUrl { get; private set; }

        /// <summary>
        /// Compresses a PlantUML source using PlantUML Text Encoding algorithm.
        /// </summary>
        /// <returns>The compressed text.</returns>
        private string CompressSourceToPlantUmlUrl()
        {
            byte[] buffer = Encoding.UTF8.GetBytes(this.source);
            MemoryStream memoryStream = new MemoryStream();
            using (DeflateStream deflate = new DeflateStream(memoryStream, CompressionMode.Compress, true))
            {
                deflate.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            byte[] compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            char[] base64 = Convert.ToBase64String(memoryStream.ToArray()).ToArray();
            char[] mapped = new char[base64.Length];

            string base64mapping = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            string PlantUMLmapping = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-_ ";

            for (int i = 0; i < base64.Length; i++)
            {
                int mapIndex = base64mapping.IndexOf(base64[i]);

                mapped[i] = PlantUMLmapping.ElementAt(mapIndex);
            }

            return new string(mapped).TrimEnd();
        }
    }
}
