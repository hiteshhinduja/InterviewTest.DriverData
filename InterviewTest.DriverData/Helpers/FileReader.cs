using InterviewTest.DriverData.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class FileReader : IContentReader
    {
        /// <summary>
        /// Reads data from the file if available at given source
        /// </summary>
        /// <param name="source"></param>
        /// <returns>File data in string</returns>
        public string ReadData(string source)
        {
            try
            {
                var content = File.ReadAllText(source);
                return content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while reading the file {source}. {ex.Message}");
            }
        }
    }
}
