# Layer

Namespace: NeuralNetwork

Implements a layer representation in a neural network structure that contains multiple [Neuron](./neuralnetwork.neuron.md)s which get identified by their local index.

```csharp
public class Layer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Layer](./neuralnetwork.layer.md)

## Fields

### **Name**

An optional name attribute to make it easier to debug and prettier for export and import (e.g. InputLayer, OutputLayer, Hidden-N-Layer,...).

```csharp
public string Name;
```

### **NeuronList**

The [Neuron](./neuralnetwork.neuron.md)s which reside in the layer and are addressed by their index.

```csharp
public List<Neuron> NeuronList;
```

## Constructors

### **Layer(Int32, Layer)**

Initializes a new instance of a fully connected layer with zero initialized [Neuron](./neuralnetwork.neuron.md)s which get all connected with [Dendrite](./neuralnetwork.dendrite.md)s to a [Layer](./neuralnetwork.layer.md) behind it if supplied.

```csharp
public Layer(int neuronCount, Layer boundLayer)
```

#### Parameters

`neuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The amount of neurons the layer will have

`boundLayer` [Layer](./neuralnetwork.layer.md)<br>
A layer behind the newly constructed one which neurons will get dendrite connections to the new layer

**Remarks:**

To create the input layer or to not create fully connected layers supply null as boundLayer for manual dendrite connections.
