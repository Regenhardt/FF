using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace NeuralNetwork.EvolutionaryAlgorithm
{
    /// <summary>
    /// Implements an evolutionary algorithm that generates genomes (AI instances) that go through evolutions which can be manipulated through the parameters and their determined fitness.
    /// </summary>
    public class EvoAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the algorithm that creates and populates <see cref="Genome" />s with the given properties.
        /// </summary>
        /// <param name="genomeCount">Determines the amount of genomes (population size)</param>
        /// <param name="inputCount">Sets the amount of input neurons / values for the genomes</param>
        /// <param name="outputCount">Sets the amount of output neurons / values for the genomes</param>
        /// <param name="hiddenLayerCount">Sets the amount of hidden layers for the genomes</param>
        /// <param name="hiddenNeuronCount">Sets the amount of hidden neurons for the genomes</param>
        /// <param name="randomSeed">Sets the random seed for the random generator which is used for value population and mutation in the genomes</param>
        /// <remarks>The algorithm uses 1 central instance of a random number generator, therefore the same random seed with the same parameters and genome fitness's will result in the same result / populations.</remarks>
        public EvoAlgorithm(int genomeCount, int inputCount, int outputCount, int hiddenLayerCount, int hiddenNeuronCount, int randomSeed)
        {
            _random = new Random(randomSeed);
            _genomes = new List<Genome>();

            for (var i = 0; i < genomeCount; i++)
            {
                _genomes.Add(new Genome(inputCount, hiddenNeuronCount, outputCount, hiddenLayerCount, _random));
            }
        }

        /// <summary>
        /// Allows access to the specified <see cref="NeuralNetwork.EvolutionaryAlgorithm.Genome" />.
        /// </summary>
        /// <param name="id">The genome identifier (Range: [0 - (genomeCount - 1)])</param>
        /// <returns>Genome with the given id</returns>
        /// <remarks> Evolutions creates genomes and puts them into different positions (different id) even if they are kept because of bestPerformerKeepRate.</remarks>
        public Genome GetMyGenome(int id)
        {
            return _genomes[id];
        }

        /// <summary>
        /// Repopulates the entire genomes with the given parameters and randomness.
        /// Preferences regarding which genomes according to their fitness rating must be kept and which should be preferred or excluded can be controlled via the parameter. 
        /// </summary>
        /// <param name="bestPerformerKeepRate">The percentage of the top performers that will remain untouched (0.0 - 1.0)</param>
        /// <param name="thresholdFitness">The minimal amount of fitness the genomes must have to be used for breeding / gene mixing and mutation.</param>
        /// <param name="minKeepThreshold">The minimal amount of genomes that must be used in the breeding process to prevent thresholdFitness from filtering out too many parents.</param>
        /// <remarks>The fitness of each genome needs to be set accordingly before calling this function.</remarks>
        public void Evolution(double bestPerformerKeepRate, int thresholdFitness, int minKeepThreshold)
        {
            var newGenomes = new List<Genome>();
            // Sort genomes and filter out those who do not meet the fitness requirements while making sure that we not too many get sorted out (minKeepThreshold).
            var sortedGenomes = _genomes.OrderBy(g => -g.Fitness).ToList();
            var breedingParticipants = Math.Max(_genomes.Count(genome => genome.Fitness > thresholdFitness), minKeepThreshold);

            // Keep the best performer guaranteed without any modifications (They are still in the breeding pool)
            for (var i = 0; i < (int)Math.Round(_genomes.Count * bestPerformerKeepRate, 0, MidpointRounding.ToEven); i++)
                newGenomes.Add(sortedGenomes[i]);

            // Get random genome pairs (to fill up the gene pool again) and breed them
            foreach (var parents in RandomUtils.GetRandomUniquePairs((_genomes.Count - newGenomes.Count) / 2, breedingParticipants, _random))
            {
                newGenomes.Add(new Genome(sortedGenomes[parents[0]]).RandomlyInheritGenes(sortedGenomes[parents[1]], _random).MutateRandomly(_random));
                newGenomes.Add(new Genome(sortedGenomes[parents[1]]).RandomlyInheritGenes(sortedGenomes[parents[0]], _random).MutateRandomly(_random));
            }

            _genomes = newGenomes;
        }

        private readonly Random _random;
        private List<Genome> _genomes;
    }
}
