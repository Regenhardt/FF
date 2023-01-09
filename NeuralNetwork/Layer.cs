using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Layer
    {
        public Layer(int neuronCount, Layer boundLayer)
        {
            NeuronList = new List<Neuron>();

            for (var i = 0; i < neuronCount; i++)
            {
                NeuronList.Add(new Neuron());
            }

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
        public string Name { get; set; }

        public List<Neuron> NeuronList;
    }
}
