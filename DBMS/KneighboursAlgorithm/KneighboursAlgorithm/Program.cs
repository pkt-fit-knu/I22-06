using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace KneighboursAlgorithm
{
     
    class Program
    {
        public static string[] endings = new string[] { "а", "е", "у", "ы", "и", "о", "я", "ю", "е", "ё", "ь", "й" };
        static void Main(string[] args)
        {

            ArrayList text1 = new ArrayList();
            ArrayList text2 = new ArrayList();
            ArrayList text3 = new ArrayList();
            ArrayList text4 = new ArrayList();
            ArrayList text5 = new ArrayList();
            ArrayList text6 = new ArrayList();
            ArrayList text7 = new ArrayList();
            ArrayList text8 = new ArrayList();
            ArrayList text9 = new ArrayList();
            ArrayList text10 = new ArrayList();
            ArrayList text11 = new ArrayList();
            ArrayList text12 = new ArrayList();
            ArrayList[] texts = new ArrayList[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12 };

            //StringBuilder line = new StringBuilder("",256);
            
            string line1,line2,line3,line4,line5,line6,line7,line8,line9,line10,line11,line12;
            
#region filePaths  
                line1 = "sports1.txt";
                line2 = "sports2.txt";
                line3 = "sports3.txt";
                line4 = "sports4.txt";
                line5 = "sports5.txt";
                line6 = "sports6.txt";
                line7 = "politics1.txt";
                line8 = "politics2.txt";
                line9 = "poliics3.txt";
                line10 = "politics4.txt";
                line11 = "politics5.txt";
                line12 = "politics6.txt";
#endregion

            string[] filePaths = new string[] { line1,line2,line3,line4,line5,line6,line7,line8,line9,line10,line11,line12};
               
            
                AddWords(ref text1, line1);
                AddWords(ref text2, line2);
                AddWords(ref text3, line3);
                AddWords(ref text4, line4);
                AddWords(ref text5, line5);
                SortedList<string, int> sportsWords = new SortedList<string, int>();
                for (int i = 0; i < 5; i++)
                {
                    String[] text = (String[])texts[i].ToArray(typeof(string));
                    for (int j = 0; j < text.Length; j++)
                    {
                        if (sportsWords.ContainsKey(text[j]))
                        {
                            int indexOfKey = sportsWords.IndexOfKey(text[j]);
                            int valueOfKey;
                            sportsWords.TryGetValue(text[j], out valueOfKey);
                            valueOfKey++;
                            sportsWords.Remove(text[j]);
                            sportsWords.Add(text[j], valueOfKey);

                        }
                        else
                        {
                            sportsWords.Add(text[j], 1);
                        }
                    }
                }
                foreach (var word in sportsWords)
                {
                    Console.WriteLine(word.Key + " " + word.Value);
                }
                //foreach(var item in text1)
                //{
                //    Console.WriteLine(item);
                //}
                //foreach (var item in text2)
                //{
                //    Console.WriteLine(item);
                //}
                //foreach (var item in text3)
                //{
                //    Console.WriteLine(item);
                //}
                //foreach (var item in text4)
                //{
                //    Console.WriteLine(item);
                //}
                //foreach (var item in text5)
                //{
                //    Console.WriteLine(item);
                //}
                Console.WriteLine(text1.Count+text2.Count+text3.Count+text4.Count+text5.Count);
            Console.ReadLine();

        }

        static void AddWords(ref ArrayList list, string filePath )
        {
            try
            {
                string line;
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr1 = new StreamReader(filePath);
               line = sr1.ReadLine();

               
                //Continue to read until you reach end of file
                while (line != null)
                {
                    string[] a = CleanInput(line).Split(' ');
                    for (int i = 0; i < a.Length; i++)
                    {
                        string word = a[i];
                        for (int t = 0; t < word.Length; t++)
                        {

                            for (int j = 0; j < endings.Length; j++)
                            {
                                if (word.Length > 1)
                                {
                                    if (endings[j].Equals(word.Substring(word.Length - 1, 1)))
                                    {
                                        string temp = word.Substring(0, word.Length - 1);
                                        word = temp;
                                    }
                                }
                            }
                        }
                        if (word.Length > 2)
                            list.Add(word);
                        //Console.WriteLine(word.ToString());
                    }
                    line = sr1.ReadLine();
                  }
                
                    //write the line to console window
                    Console.WriteLine(CleanInput(line));
                    sr1.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }



                    
           

        }
        static string CleanInput(string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                string strNum = Regex.Replace(strIn, @"[^\w\s]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));

                return Regex.Replace(strNum, @"[0-9]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters, 
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }


    }
}
