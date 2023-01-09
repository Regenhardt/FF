# Dendrite

Namespace: NeuralNetwork

Implements a dendrite representation in a neural network structure that connects 2 [Neuron](./neuralnetwork.neuron.md)s together.

```csharp
public class Dendrite
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Dendrite](./neuralnetwork.dendrite.md)

**Remarks:**

A dendrite always connects a [Neuron](./neuralnetwork.neuron.md)s from 1 [Layer](./neuralnetwork.layer.md) to a previous [Layer](./neuralnetwork.layer.md).

## Fields

### **Weight**

The weight between the two [Neuron](./neuralnetwork.neuron.md)s.

```csharp
public double Weight;
```

### **ConnectedLayer**

layer in which the connected [Neuron](./neuralnetwork.neuron.md)s resides.

```csharp
public Layer ConnectedLayer;
```

**Remarks:**

This attribute will not get serialized as this would lead to a nested serialization cycle.

### **Neuron**

The identifier for the connected [Neuron](./neuralnetwork.neuron.md) in the connected [Layer](./neuralnetwork.layer.md).

```csharp
public int Neuron;
```

## Constructors

### **Dendrite(Layer, Int32)**

Initializes a new instance of a dendrite with an initial [Dendrite.Weight](./neuralnetwork.dendrite.md#weight) of zero that connects two [Neuron](./neuralnetwork.neuron.md)s from two different [Layer](./neuralnetwork.layer.md)s.

```csharp
public Dendrite(Layer connectedLayer, int connectedNeuron)
```

#### Parameters

`connectedLayer` [Layer](./neuralnetwork.layer.md)<br>
The layer in which the connected neuron resides

`connectedNeuron` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The identifier for the connected neuron in the connected layer
