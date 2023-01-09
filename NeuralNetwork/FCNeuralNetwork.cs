using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTests")]
namespace NeuralNetwork
{
    /// <summary>
    /// Implements a fully connected neural network structure.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class FCNeuralNetwork
    {
        #region JSON Serialization / Deserialization 
        /// <summary>
        /// Creates an instance of the supplied JSON representation.
        /// </summary>
        /// <param name="objJson">JSON Instance representation</param>
        /// <returns>Reconstructed instance</returns>
        public static FCNeuralNetwork Deserialize(string objJson)
        {
            var fcNeuralNetwork = JsonConvert.DeserializeObject<FCNeuralNetwork>(objJson);
            var firstHiddenLayer = fcNeuralNetwork.HiddenLayer.First();
            var prevLayer = fcNeuralNetwork.HiddenLayer.Last();

            foreach (var dendrite in fcNeuralNetwork.OutputLayer.NeuronList.SelectMany(neuron => neuron.ConnectedNeurons))
            {
                dendrite.ConnectedLayer = prevLayer;
            }

            for (var i = fcNeuralNetwork.HiddenLayer.Count - 1; i > 0; i--)
            {
                foreach (var dendrite in fcNeuralNetwork.HiddenLayer[i].NeuronList.SelectMany(neuron => neuron.ConnectedNeurons))
                {
                    dendrite.ConnectedLayer = fcNeuralNetwork.HiddenLayer[i-1];
                }
            }

            foreach (var dendrite in firstHiddenLayer.NeuronList.SelectMany(neuron => neuron.ConnectedNeurons))
            {
                dendrite.ConnectedLayer = fcNeuralNetwork.InputLayer;
            }

            return fcNeuralNetwork;
        }
        /// <summary>
        /// Serializes the given instance into a json representation string.
        /// </summary>
        /// <param name="fcNeuralNetwork">Instance to be serialized</param>
        /// <returns>JSON Object representation as a string</returns>
        public static string Serialize(FCNeuralNetwork fcNeuralNetwork)
        {
            return JsonConvert.SerializeObject(fcNeuralNetwork);
        }
        /// <summary>
        /// Hollow direct member assign constructor for json deserialization.
        /// </summary>
        [JsonConstructor]
        protected FCNeuralNetwork(string name, Layer inputLayer, List<Layer> hiddenLayer, Layer outputLayer)
        {
            Name = name;
            InputLayer = inputLayer;
            HiddenLayer = hiddenLayer;
            OutputLayer = outputLayer;
        }
        #endregion
        /// <summary>
        /// Initializes a new instance of a fully connected neural network with the given properties.
        /// </summary>
        /// <param name="inputNeuronCount">Sets the amount of input neurons / values</param>
        /// <param name="hiddenNeuronCount">Sets the amount of hidden neurons</param>
        /// <param name="outputNeuronCount">Sets the amount of output neurons / values</param>
        /// <param name="hiddenLayerCount">Sets the amount of hidden layers</param>
        /// <exception cref="Exception"></exception>
        public FCNeuralNetwork(int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount, int hiddenLayerCount = 1)
        {
            if (hiddenLayerCount < 1)
            {
                throw new Exception("Can not have 0 hidden layers!");
            }

            // Construct fully connected NN backwards for connections
            OutputLayer = new Layer(outputNeuronCount, null)
            {
                Name = "Output"
            };

            var prevLayer = OutputLayer;
            HiddenLayer = new List<Layer>();
            for (var i = hiddenLayerCount; i != 0; i--)
            {
                var newHiddenLayer = new Layer(hiddenNeuronCount, prevLayer)
                {
                    Name = "Hidden-" + i
                };
                HiddenLayer.Insert(0, newHiddenLayer);
                prevLayer = newHiddenLayer;
            }

            InputLayer = new Layer(inputNeuronCount, prevLayer)
            {
                Name = "Input"
            };
        }
        /// <summary>
        /// Creates a deep copy of the given neural network.
        /// </summary>
        public FCNeuralNetwork Clone()
        {
            // TODO: Dirty solution for the moment (time restraints)
            return Deserialize(Serialize(this));
        }
        /// <summary>
        /// An optional name attribute to make it easier to debug and prettier for export and import.
        /// </summary>
        public string Name;
        /// <summary>
        /// The input layer of the neural network.
        /// </summary>
        public Layer InputLayer;
        /// <summary>
        /// The hidden layers of the neural network.
        /// </summary>
        public List<Layer> HiddenLayer;
        /// <summary>
        /// The output layer of the neural network.
        /// </summary>
        public Layer OutputLayer;
    }
}
