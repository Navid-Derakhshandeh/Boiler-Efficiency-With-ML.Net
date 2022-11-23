using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Runtime.Api;


namespace Boiler_Efficiency
{
    
    class FeedBackTrainingValue
    {
        [Column(ordinal: "0", name: "Label")]
        public bool FeedBackReturn { get; set; }


        [Column(ordinal: "1")]
        public string efficiency { get; set; }
        

    }
    class FeedBackPredict
    {
        [ColumnName("PredictedLabel")]
        public bool FeedBackReturn { get; set; }
    }
    class Program
    {

        static List<FeedBackTrainingValue> trainingvalue =
            new List<FeedBackTrainingValue>();
        static List<FeedBackTrainingValue> exValue =
            new List<FeedBackTrainingValue>();
        static void LoadexValue()
        {
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "56089.3333",
                FeedBackReturn = true

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "0.18736732",
                FeedBackReturn = false

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "123.8632",
                FeedBackReturn = true

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-0.86722",
                FeedBackReturn = false

            });
        }
        static void Loadtrainingvalue()
        {
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "11113.5989933591778",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "563.59899335",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "118.667889355",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-23.18736732",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "0.18736732",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-0.86722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "2.11186722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "1.23386722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "5.822226722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "7.83336722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "8.844446722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "9.811167262",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-12.84446722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-10.843336722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-11.83336722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-14.85556722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "10.868767722",
                FeedBackReturn = false

            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "1093678.36578829",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "767",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "56782.55",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "-8.2672",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "76245.972",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "23.8267",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "99999.99999",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "679.99999",
                FeedBackReturn = true
            });
        }
        static void Main(string[] args)
        {
            
            ConsoleKeyInfo conkey;
            do
            {


                try
                {
                    Console.WriteLine("              ##########################             ");
                    Console.WriteLine("                ######################             ");
                    Console.WriteLine("                    ##############                 ");
                    Console.WriteLine("        n=(Energy output)/(Energy input) X 100                                    ");
                    Console.WriteLine("                E= [Q (H-h)/q*GCV]*100                                  ");
                    Console.WriteLine("*If Machine Was True It's Mean Your Boiler is Efficient*");
                    Console.WriteLine("*If Machine Was false It's Mean Your Boiler is not Efficient*");
                    Console.WriteLine("                    ##############                 ");
                    Console.WriteLine("                ######################             ");
                    Console.WriteLine("              ##########################             ");


                    Console.WriteLine("Please Enter The Q= Quantity of steam generated (kg/hr) of Your Energy output: ");
                    double Q = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter Your The Enthalpy of steam (Kcal/kg) of Your Energy output : ");
                    double H = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter The Enthalpy of water (kcal/kg) of Your Energy output : ");
                    double h = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter The GCV= Gross calorific value of the fuel of Your Energy Output : ");
                    double GCV = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter The q=Quantity of Consumed Fuel (kg/h) : ");
                    double q = double.Parse(Console.ReadLine());

                    double a = Q*(H - h) * 100 / (q * GCV);

                    Console.WriteLine("Please Enter The Q= Quantity of steam generated (kg/hr) of Your Energy input: ");
                    double Q1 = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter Your The Enthalpy of steam (Kcal/kg) of Your Energy input : ");
                    double H1 = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter The Enthalpy of water (kcal/kg) of Your Energy input : ");
                    double h1 = double.Parse(Console.ReadLine());
                    Console.WriteLine("Please Enter The GCV= Gross calorific value of the fuel of Your Energy input : ");
                    double GCV1 = double.Parse(Console.ReadLine());

                    double b = Q1 * (H1 - h1) * 100 / (Q1 * GCV1);

                    double n = a / b * 100;

                    Console.WriteLine("Your Boiler Efficiency Equal By : {0}", n);

                    Loadtrainingvalue();

                    var modelContext = new MLContext();

                    Microsoft.ML.Runtime.Data.IDataView dataView = modelContext.CreateStreamingDataView<FeedBackTrainingValue>(trainingvalue);

                    var Datapipeline = modelContext.Transforms.Text.FeaturizeText("efficiency", "Features")
                        .Append(modelContext.BinaryClassification.Trainers.FastTree(numLeaves: 50, numTrees: 50, minDatapointsInLeaves: 1));
                    var Model = Datapipeline.Fit(dataView);

                    LoadexValue();

                    Microsoft.ML.Runtime.Data.IDataView dataView1 = modelContext.CreateStreamingDataView<FeedBackTrainingValue>(exValue);

                    var Predict = Model.Transform(dataView1);
                    var Scale = modelContext.BinaryClassification.Evaluate(Predict, "Label");

                    Console.WriteLine(Scale.Accuracy);

                    

                    string efficiency1 = n.ToString();
                    var predictFunc = Model.MakePredictionFunction<FeedBackTrainingValue, FeedBackPredict>(modelContext);
                    var FBInput = new FeedBackTrainingValue();
                    FBInput.efficiency = efficiency1;
                    var fbpredict = predictFunc.Predict(FBInput);
                    Console.WriteLine("Machine : " + fbpredict.FeedBackReturn);

                
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                finally
                {
                    Console.WriteLine("Enter Any Key That You Want, For Run Program Again.");
                }

                conkey = Console.ReadKey();
            } while (conkey.Key != ConsoleKey.Escape);
        
        }
    }
}

