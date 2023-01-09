# EvoAlgorithm

Namespace: NeuralNetwork.EvolutionaryAlgorithm

Implements an evolutionary algorithm that generates genomes (AI instances) that go through evolutions which can be manipulated through the parameters and their determined fitness.

```csharp
public class EvoAlgorithm
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [EvoAlgorithm](./neuralnetwork.evolutionaryalgorithm.evoalgorithm.md)

## Constructors

### **EvoAlgorithm(Int32, Int32, Int32, Int32, Int32, Int32)**

Initializes a new instance of the algorithm that creates and populates [Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)s with the given properties.

```csharp
public EvoAlgorithm(int genomeCount, int inputCount, int outputCount, int hiddenLayerCount, int hiddenNeuronCount, int randomSeed)
```

#### Parameters

`genomeCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Determines the amount of genomes (population size)

`inputCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of input neurons / values for the genomes

`outputCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of output neurons / values for the genomes

`hiddenLayerCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden layers for the genomes

`hiddenNeuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden neurons for the genomes

`randomSeed` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the random seed for the random generator which is used for value population and mutation in the genomes

**Remarks:**

The algorithm uses 1 central instance of a random number generator, therefore the same random seed with the same parameters and genome fitness's will result in the same result / populations.

## Methods

### **GetMyGenome(Int32)**

Allows access to the specified [Genome](./neuralnetwork.evolutionaryalgorithm.genome.md).

```csharp
public Genome GetMyGenome(int id)
```

#### Parameters

`id` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The genome identifier (Range: [0 - (genomeCount - 1)])

#### Returns

[Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)<br>
Genome with the given id

**Remarks:**

Evolutions creates genomes and puts them into different positions (different id) even if they are kept because of bestPerformerKeepRate.

### **Evolution(Double, Int32, Int32)**

Repopulates the entire genomes with the given parameters and randomness.
 Preferences regarding which genomes according to their fitness rating must be kept and which should be preferred or excluded can be controlled via the parameter.

```csharp
public void Evolution(double bestPerformerKeepRate, int thresholdFitness, int minKeepThreshold)
```

#### Parameters

`bestPerformerKeepRate` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The percentage of the top performers that will remain untouched (0.0 - 1.0)

`thresholdFitness` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The minimal amount of fitness the genomes must have to be used for breeding / gene mixing and mutation.

`minKeepThreshold` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The minimal amount of genomes that must be used in the breeding process to prevent thresholdFitness from filtering out too many parents.

**Remarks:**

The fitness of each genome needs to be set accordingly before calling this function.
