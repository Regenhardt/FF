using System;
using System.Linq;
using Utils;

namespace NeuralNetwork.EvolutionaryAlgorithm
{
    /// <summary>
    /// Implements a genome representation that encapsulates a fully connect neural network (<see cref="FCNeuralNetwork" />) combined with a tracked fitness.
    /// </summary>
    public class Genome
    {
        /// <summary>
        /// Initializes a new instance of a genome (<see cref="FCNeuralNetwork" />) with the given properties.
        /// </summary>
        /// <param name="inputCount">Sets the amount of input neurons / values for the genome</param>
        /// <param name="hiddenNeuronCount">Sets the amount of hidden neurons for the genome</param>
        /// <param name="outputCount">Sets the amount of output neurons / values for the genome</param>
        /// <param name="hiddenLayerCount">Sets the amount of hidden layers for the genome</param>
        /// <param name="random">Random number generator that is used to randomly populate the weights and biases</param>
        public Genome(int inputCount, int hiddenNeuronCount, int outputCount, int hiddenLayerCount, Random random)
        {
            Fitness = 0.0;
            _fcNeuralNetwork = new FCNeuralNetwork(inputCount, hiddenNeuronCount, outputCount, hiddenLayerCount);

            foreach (var layer in _fcNeuralNetwork.HiddenLayer)
            {
                LayerUtils.PopulateLayerRandomly(layer, random);
            }

            LayerUtils.PopulateLayerRandomly(_fcNeuralNetwork.OutputLayer, random);
        }

        /// <summary>
        /// Initializes a deep copy of the given genomes neural network while initializing the fitness with zero.
        /// </summary>
        /// <param name="copy">Genome to be copied</param>
        public Genome(Genome copy)
        {
            Fitness = 0.0;
            _fcNeuralNetwork = new FCNeuralNetwork(copy._fcNeuralNetwork);
        }

        /// <summary>
        /// Randomly inherits (changes) the biases and weights with the ones from the given genome.
        /// </summary>
        /// <param name="otherGenome">Bias and weight genome source</param>
        /// <param name="random">Random number generator that is used to randomly determine the biases and weights which should be inherited</param>
        /// <returns>Target genome for daisy chaining operations</returns>
        /// <remarks>Source and target genome must have the same topology!</remarks>
        public Genome RandomlyInheritGenes(Genome otherGenome, Random random) 
        {
            for (var i = 0; i < _fcNeuralNetwork.HiddenLayer.Count; i++)
            {
                LayerUtils.InheritLayerBiasAndWeightsRandomly(_fcNeuralNetwork.HiddenLayer[i], otherGenome._fcNeuralNetwork.HiddenLayer[i], random);
            }

            LayerUtils.InheritLayerBiasAndWeightsRandomly(_fcNeuralNetwork.OutputLayer, otherGenome._fcNeuralNetwork.OutputLayer, random);

            return this;
        }

        /// <summary>
        /// Mutates biases and weights randomly with random values.
        /// </summary>
        /// <param name="random">Random number generator that is used to randomly determine the biases and weights which should be mutated and if so to generate their new values</param>
        /// <returns>Target genome for daisy chaining operations</returns>
        public Genome MutateRandomly(Random random)
        {
            foreach (var layer in _fcNeuralNetwork.HiddenLayer)
            {
                LayerUtils.MutateLayerRandomly(layer, random);
            }

            LayerUtils.MutateLayerRandomly(_fcNeuralNetwork.OutputLayer, random);

            return this;
        }

        /// <summary>
        /// Processes an input through the internal neural network.
        /// </summary>
        /// <param name="inputs">Input values for the neural network</param>
        /// <returns>Output layer values</returns>
        public double[] Process(double[] inputs)
        {
            // Set input values in the input layer
            for (var i = 0; i < inputs.Length; i++)
            {
                _fcNeuralNetwork.InputLayer.NeuronList[i].Value = inputs[i];
            }
            
            // Calculate hidden Layer
            foreach (var neuron in _fcNeuralNetwork.HiddenLayer.SelectMany(layer => layer.NeuronList))
            {
                neuron.Value = 0;
                foreach (var dendrite in neuron.ConnectedNeurons)
                {
                    neuron.Value += dendrite.ConnectedLayer.NeuronList[dendrite.Neuron].Value * dendrite.Weight;
                }
                neuron.Value = MathUtils.Sigmoid(neuron.Bias + neuron.Value);
            }

            // Calculate output layer
            foreach (var neuron in _fcNeuralNetwork.OutputLayer.NeuronList)
            {
                neuron.Value = 0;
                foreach (var dendrite in neuron.ConnectedNeurons)
                {
                    neuron.Value += dendrite.ConnectedLayer.NeuronList[dendrite.Neuron].Value * dendrite.Weight;
                }
                neuron.Value = Math.Tanh(neuron.Bias + neuron.Value);
            }

            // Collect output from output layer
            var outputs = new double[_fcNeuralNetwork.OutputLayer.NeuronList.Count];
            for (var i = 0; i < _fcNeuralNetwork.OutputLayer.NeuronList.Count; i++)
            {
                outputs[i] = _fcNeuralNetwork.OutputLayer.NeuronList[i].Value;
            }
            return outputs;
        }

        /// <summary>
        /// Genome / neural network fitness.
        /// </summary>
        public double Fitness;
        private readonly FCNeuralNetwork _fcNeuralNetwork;
    }
}
