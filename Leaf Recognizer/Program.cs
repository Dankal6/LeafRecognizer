// <SnippetAddUsings>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
// </SnippetAddUsings>

namespace TransferLearningTF
{
    class Program
    {
        static readonly string _assetsPath = Path.Combine(Environment.CurrentDirectory, "assets");
        static readonly string _imagesFolder = Path.Combine(_assetsPath, "images");
        static readonly string _trainTagsTsv = Path.Combine(_imagesFolder, "tags.tsv");
        static readonly string _testTagsTsv = Path.Combine(_imagesFolder, "test-tags.tsv");
        static readonly string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");

        static void Main(string[] args)
        {
            // Create MLContext to be shared across the model creation workflow objects
            MLContext mlContext = new MLContext();

            ITransformer model = GenerateModel(mlContext);

            ClassifySingleImage(mlContext, model);

            Console.ReadKey();
        }

        // Build and train model
        public static ITransformer GenerateModel(MLContext mlContext)
        {
            IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
                            // The image transforms transform the images into the model's expected format.
                            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
                            // </SnippetImageTransforms>
                            // The ScoreTensorFlowModel transform scores the TensorFlow model and allows communication
                            .Append(mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).
                                ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
                            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
                            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
                            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                            .AppendCacheCheckpoint(mlContext);

            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);

            // Train the model
            Console.WriteLine("=============== Training classification model ===============");
            // Create and train the model
            ITransformer model = pipeline.Fit(trainingData);

            // Generate predictions from the test data, to be evaluated
            IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);
            IDataView predictions = model.Transform(testData);

            // Create an IEnumerable for the predictions for displaying results
            IEnumerable<ImagePrediction> imagePredictionData = mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, true);
            DisplayResults(imagePredictionData);

            // Get performance metrics on the model using training data
            Console.WriteLine("=============== Classification metrics ===============");

            MulticlassClassificationMetrics metrics =
                mlContext.MulticlassClassification.Evaluate(predictions,
                  labelColumnName: "LabelKey",
                  predictedLabelColumnName: "PredictedLabel");

            Console.WriteLine($"LogLoss is: {metrics.LogLoss}");
            Console.WriteLine($"PerClassLogLoss is: {String.Join(" , ", metrics.PerClassLogLoss.Select(c => c.ToString()))}");

            return model;
        }

        public static void ClassifySingleImage(MLContext mlContext, ITransformer model)
        {
            Console.WriteLine("=============== Making single image classification ===============");

            for (int i = 0; i < 7; i++)
            {
                // load the fully qualified image file name into ImageData
                var imageData = new ImageData()
                {
                    ImagePath = "test" + i.ToString() + ".jpg"
                };

                // Make prediction function (input = ImageData, output = ImagePrediction)
                var predictor = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
                var prediction = predictor.Predict(imageData);

                Console.WriteLine($"Image: {Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            }
        }

        private static void DisplayResults(IEnumerable<ImagePrediction> imagePredictionData)
        {
            foreach (ImagePrediction prediction in imagePredictionData)
            {
                Console.WriteLine($"Image: {Path.GetFileName(prediction.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            }
        }

        public static IEnumerable<ImageData> ReadFromTsv(string file, string folder)
        {
            //Need to parse through the tags.tsv file to combine the file path to the
            // image name for the ImagePath property so that the image file can be found.

            return File.ReadAllLines(file)
             .Select(line => line.Split('\t'))
             .Select(line => new ImageData()
             {
                 ImagePath = Path.Combine(folder, line[0])
             });
        }

        private struct InceptionSettings
        {
            public const int ImageHeight = 224;
            public const int ImageWidth = 224;
            public const float Mean = 117;
            public const float Scale = 1;
            public const bool ChannelsLast = true;
        }

        public class ImageData
        {
            [LoadColumn(0)]
            public string ImagePath;

            [LoadColumn(1)]
            public string Label;
        }

        public class ImagePrediction : ImageData
        {
            public float[] Score;

            public string PredictedLabelValue;
        }
    }
}
