using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NeuralNetwork
{
    /// <summary>
    /// Implements a layer representation in a neural network structure that contains multiple <see cref="Neuron" />s which get identified by their local index.
    /// </summary>
    internal class Layer
    {
        /// <summary>
        /// Initializes a new instance of a fully connected layer with zero initialized <see cref="Neuron" />s which get all connected with <see cref="Dendrite" />s to a <see cref="Layer" /> behind it if supplied.
        /// </summary>
        /// <param name="neuronCount">The amount of neurons the layer will have</param>
        /// <param name="boundLayer">A layer behind the newly constructed one which neurons will get dendrite connections to the new layer</param>
        /// <remarks>To create the input layer or to not create fully connected layers supply null as boundLayer for manual dendrite connections.</remarks>
        public Layer(int neuronCount, Layer boundLayer)
        {
            NeuronList = new List<Neuron>(neuronCount);

            for (var i = 0; i < neuronCount; i++)
            {
                NeuronList.Add(new Neuron());
            }

            // ReSharper disable once InvertIf
            if (boundLayer != null)
            {
                foreach (var neuron in boundLayer.NeuronList)
                {
                    for (var i = 0; i < NeuronList.Count; i++)
                    {
                        neuron.ConnectedNeurons.Add(new Dendrite(this, i));
                    }
                }
            }
        }
        /// <summary>
        /// An optional name attribute to make it easier to debug and prettier for export and import (e.g. InputLayer, OutputLayer, Hidden-N-Layer,...).
        /// </summary>
        [IgnoreDataMember]
        public string Name;
        /// <summary>
        /// The <see cref="Neuron" />s which reside in the layer and are addressed by their index. 
        /// </summary>
        public List<Neuron> NeuronList;
    }
}
