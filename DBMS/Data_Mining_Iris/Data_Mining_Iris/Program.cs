using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Mining_Iris
{
    #region defining comparers for Iris class
    public class SepalLengthComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Iris iris1 = x as Iris;
            Iris iris2 = y as Iris;

            if (iris1 != null && iris2 != null)
            {
                if (iris1.SepalLength > iris2.SepalLength)
                    return 1;
                if (iris1.SepalLength < iris2.SepalLength)
                    return -1;
                else
                    return 0;
            }
            else throw new ArgumentException("Parameter is not an Iris!");   
            
        }
    }

    public class SepalWidthComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Iris iris1 = x as Iris;
            Iris iris2 = y as Iris;

            if (iris1 != null && iris2 != null)
            {
                if (iris1.SepalWidth > iris2.SepalWidth)
                    return 1;
                if (iris1.SepalWidth < iris2.SepalWidth)
                    return -1;
                else
                    return 0;
            }
            else throw new ArgumentException("Parameter is not an Iris!");

        }
    }

    public class PetalLengthComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Iris iris1 = x as Iris;
            Iris iris2 = y as Iris;

            if (iris1 != null && iris2 != null)
            {
                if (iris1.PetalLength > iris2.PetalLength)
                    return 1;
                if (iris1.PetalLength < iris2.PetalLength)
                    return -1;
                else
                    return 0;
            }
            else throw new ArgumentException("Parameter is not an Iris!");

        }
    }

    public class PetalWidthComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Iris iris1 = x as Iris;
            Iris iris2 = y as Iris;

            if (iris1 != null && iris2 != null)
            {
                if (iris1.PetalWidth > iris2.PetalWidth)
                    return 1;
                if (iris1.PetalWidth < iris2.PetalWidth)
                    return -1;
                else
                    return 0;
            }
            else throw new ArgumentException("Parameter is not an Iris!");

        }
    }

    #endregion

    #region Iris class definition
    public class Iris
    {
        private Double sepalLength;

        public Double SepalLength
        {
            get { return sepalLength; }
            set 
            { 
                if (value > 0)
                sepalLength = value; 
            }
        }
        private Double sepalWidth;

        public Double SepalWidth
        {
            get { return sepalWidth; }
            set {
                if (value > 0)
                sepalWidth = value; 
            }
        }
        private Double petalLength;

        public Double PetalLength
        {
            get { return petalLength; }
            set
            {
                if (value > 0)
                petalLength = value; 
            }
        }
        private Double petalWidth;

        public Double PetalWidth
        {
            get { return petalWidth; }
            set 
            {
                if (value > 0)
                petalWidth = value; 
            }
        }
        private string irisClass;

        public string IrisClass
        {
            get { return irisClass; }
            set { irisClass = value; }
        }

        public static IComparer SortBySepalLength
        { get { return (IComparer)new SepalLengthComparer(); } }

        public static IComparer SortBySepalWidth
        { get { return (IComparer)new SepalWidthComparer(); } }

        public static IComparer SortByPetalLength
        { get { return (IComparer)new PetalLengthComparer(); } }

        public static IComparer SortByPetalWidth
        { get { return (IComparer)new PetalWidthComparer(); } }
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {

            int counter = 0;
            int setosaCount = 0;
            int versicolorCount = 0;
            int virginicaCount = 0;
            int errorCount = 0;
            string line;

            char[] separator = new char[1];
            separator[0] = ',';

            
            ArrayList irisList = new ArrayList();
            #region Getting data from file and adding it to irisList
            // Read the file: iris data without iris class column
            System.IO.StreamReader file =
               new System.IO.StreamReader("C:\\Users\\Александр\\Documents\\Visual Studio 2013\\Projects\\Data_Mining_Iris\\Data_Mining_Iris\\res\\iris_with_names.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line != "")
                {
                    string[] iris = new string[5];



                    for (int i = 0; i < line.Split(separator).Length; i++)
                    {

                        iris[i] = line.Split(separator)[i];

                        Console.WriteLine(iris[i]);
                    }

                    for (int i = 0; i < iris.Length - 1; i++)
                    {
                        if (iris[i].Contains("."))
                        {
                            string a = iris[i].Substring(0, iris[i].IndexOf("."));
                            string b = iris[i].Substring(iris[i].IndexOf(".") + 1);
                            iris[i] = a + "," + b;
                        }
                    }

                    Iris tempIris = new Iris();

                    tempIris.SepalLength = Convert.ToDouble(iris[0]);
                    tempIris.SepalWidth = Double.Parse(iris[1]);
                    tempIris.PetalLength = Double.Parse(iris[2]);
                    tempIris.PetalWidth = Double.Parse(iris[3]);
                    tempIris.IrisClass = iris[4];

                    irisList.Add(tempIris);

                    //if (double.Parse(iris[3]) < 1)
                    //{
                    //    iris[4] = "Iris-setosa";
                    //    setosaCount++;
                    //}
                    //else if (1 <= double.Parse(iris[3]) && double.Parse(iris[3]) <= 1.4)

                    counter++;
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            #endregion

            //copying irisList of irises to array 
            Iris[] irisArray = (Iris[])irisList.ToArray( typeof(Iris) );
            
            Array.Sort(irisArray,new PetalWidthComparer());



            List<Iris> irisRuleList = new List<Iris>();

            //foreach (Iris item in irisArray)
            //{
            //    Console.WriteLine(item.SepalLength+" "+item.IrisClass);
            //}



            #region making list of chosen irises to make rules for classification
            for (int i = 0; i < irisArray.Length; i++)
            {
                //check if counter less than 3
                if ( Math.Max(setosaCount,Math.Max(versicolorCount,virginicaCount)) < 3)
	            {
                    if (irisArray[i].IrisClass == "Iris-setosa")
                    {
                       setosaCount++;
               
                   
                    }
                    else if (irisArray[i].IrisClass == "Iris-versicolor")
                    {
                       versicolorCount++;
                    
                    }
                    else if (irisArray[i].IrisClass == "Iris-virginica")
                    {
                       virginicaCount++;
                    
                    }
		 
	            } 
                else 
                {
                    if ((i < irisArray.Length - 1) && (irisArray[i].IrisClass != irisArray[i + 1].IrisClass))
                    {
                        Iris temp = new Iris();
                        temp = irisArray[i];
                        temp.PetalWidth = (irisArray[i].PetalWidth + irisArray[i + 1].PetalWidth) / 2;

                        irisRuleList.Add(temp);

                        errorCount += setosaCount + virginicaCount + versicolorCount - 3;
                        setosaCount = 0;
                        virginicaCount = 0;
                        versicolorCount = 0;
                    }
                    else if(i == irisArray.Length - 1)
                    {
                        
                            //it supposed to be the last element in irisArray 
                            irisRuleList.Add(irisArray[i]);
                        
                        
                    } 
                    
                    continue;
                }

            }
            #endregion

            Iris[] irisRuleArray = (Iris[])irisRuleList.ToArray();

            Console.WriteLine("SepalLength");
            for (int i = 0; i < irisRuleArray.Length; i++)
            {
                
                    if ((i < irisRuleArray.Length - 1) && (irisRuleArray[i].IrisClass != irisRuleArray[i + 1].IrisClass))
                    {
                        Console.WriteLine(irisRuleArray[i].IrisClass + " < " + (irisRuleArray[i].PetalWidth + irisRuleArray[i + 1].PetalWidth) / 2);
                        Console.Write((irisRuleArray[i].PetalWidth + irisRuleArray[i + 1].PetalWidth) / 2 + " < ");
                    }
                    else if (i == irisRuleArray.Length - 1)
                    {

                        Console.WriteLine((irisRuleArray[i - 1].PetalWidth + irisRuleArray[i].PetalWidth) / 2 + " < " + irisRuleArray[i].IrisClass);
                        
                    }

                    continue;
                

            }
            Console.WriteLine("PetalWidth errors:"+ errorCount + "(" + Math.Round(100*(double)errorCount/(double)irisArray.Length,3) + "%)");
            file.Close();


            
            // Suspend the screen.
            Console.ReadLine();

        }
    }
}
