using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    public class FCNeuralNetwork
    {
        public static FCNeuralNetwork Deserialize(string objJSON)
        {
            FCNeuralNetwork fcNeuralNetwork = JsonConvert.DeserializeObject<FCNeuralNetwork>(objJSON);

            // TODO: Make it support multiple hidden layer
            foreach (var neuron in fcNeuralNetwork.OutputLayer.NeuronList)
            {
                foreach (var dendrite in neuron.ConnectedNeurons)
                {
                    dendrite.ConnectedLayer = fcNeuralNetwork.HiddenLayer[0];
                }
            }

            // adjust this too
            foreach (var neuron in fcNeuralNetwork.HiddenLayer[0].NeuronList)
            {
                foreach (var dendrite in neuron.ConnectedNeurons)
                {
                    dendrite.ConnectedLayer = fcNeuralNetwork.InputLayer;
                }
            }

            return fcNeuralNetwork;
        }

        public static string Serialize(FCNeuralNetwork fcNeuralNetwork)
        {
            return JsonConvert.SerializeObject(fcNeuralNetwork);
        }

        [JsonConstructor]
        protected FCNeuralNetwork(string name, Layer inputLayer, List<Layer> hiddenLayer, Layer outputLayer)
        {
            Name = name;
            InputLayer = inputLayer;
            HiddenLayer = hiddenLayer;
            OutputLayer = outputLayer;
        }

        public FCNeuralNetwork(int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount, int hiddenLayerCount = 1)
        {
            if (hiddenLayerCount < 1)
            {
                throw new System.Exception("Neural net cannot have 0 hidden layers!");
            }

            // Construct fully connected NN backwards for connections
            OutputLayer = new Layer(outputNeuronCount, null);
            OutputLayer.Name = "Output";
            Layer prevLayer = OutputLayer;
            HiddenLayer = new List<Layer>();
            for (int i = hiddenLayerCount; i != 0; i--)
            {
                Layer newHiddenLayer = new Layer(hiddenNeuronCount, prevLayer);
                newHiddenLayer.Name = "Hidden-" + i.ToString();
                HiddenLayer.Add(newHiddenLayer);
                prevLayer = newHiddenLayer;
            }
            InputLayer = new Layer(inputNeuronCount, prevLayer);
            InputLayer.Name = "Input";
        }

        public FCNeuralNetwork(FCNeuralNetwork copy)
        {
            // TODO: Dirty solution for the moment
            var dirtycopy = Deserialize(Serialize(copy));
            Name = dirtycopy.Name;
            InputLayer = dirtycopy.InputLayer;
            HiddenLayer = dirtycopy.HiddenLayer;
            OutputLayer = dirtycopy.OutputLayer;
        }

        public string Name { get; set; }

        public Layer InputLayer { get; set; }

        public List<Layer> HiddenLayer { get; set; }

        public Layer OutputLayer { get; set; }
    }
}
