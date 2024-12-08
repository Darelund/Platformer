using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class FileManager
    {
        /// <summary>
        /// Reads the contents of a specified text file line by line and populates a list of strings,
        /// where each string represents a line from the file. This is useful for processing 
        /// paragraphs or multiline text, as each line will be stored as a separate element in the list.
        /// </summary>
        /// <param name="fileName">The path to the text file to be read.</param>
        /// <returns>A list of strings, where each string is a line from the file.</returns>
        public static List<string> ReadFromFile(string fileName)
        {
            List<string> result = new List<string>();

            using (StreamReader sR = new StreamReader(fileName))
            {
                while (!sR.EndOfStream)
                {
                    string line = sR.ReadLine();
                    result.Add(line);
                    // Debug.WriteLine(line);
                    //Created things in the output
                }
            }
            return result;
        }
    }
}
