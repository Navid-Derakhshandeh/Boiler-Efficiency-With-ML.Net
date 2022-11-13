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
                efficiency = "Yes",
                FeedBackReturn = true

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "No",
                FeedBackReturn = false

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "y",
                FeedBackReturn = true

            });
            exValue.Add(new FeedBackTrainingValue()
            {
                efficiency = "n",
                FeedBackReturn = false

            });
        }
        static void Loadtrainingvalue()
        {


            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "NO",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "Yes",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "n",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "yes",
                FeedBackReturn = true
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "no",
                FeedBackReturn = false
            });
            trainingvalue.Add(new FeedBackTrainingValue()
            {
                efficiency = "y",
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

                    

                   if(n >= 50)
                    {
                        string Y1 = "Yes";

                        string efficiency1 = Y1.ToString();


                        var predictFunc = Model.MakePredictionFunction<FeedBackTrainingValue, FeedBackPredict>(modelContext);
                        var FBInput = new FeedBackTrainingValue();
                        FBInput.efficiency = efficiency1;
                        var fbpredict = predictFunc.Predict(FBInput);
                        Console.WriteLine("Machine : " + fbpredict.FeedBackReturn);
                    }
                    else
                    {
                        string N1 = "No";
                        string efficiency1 = N1.ToString();
                        var predictFunc = Model.MakePredictionFunction<FeedBackTrainingValue, FeedBackPredict>(modelContext);
                        var FBInput = new FeedBackTrainingValue();
                        FBInput.efficiency = efficiency1;
                        var fbpredict = predictFunc.Predict(FBInput);
                        Console.WriteLine("Machine : " + fbpredict.FeedBackReturn);
                    }




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

