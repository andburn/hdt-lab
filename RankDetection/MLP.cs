using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RankDetection
{
	public enum LayerType
	{
		SIGMOID,
		SOFTMAX
	}

	public class Layer
	{
		public LayerType Type { get; set; }
		public List<float> Biases { get; set; }
		public Matrix<float> Weights { get; set; }
	}


	// Simple feed forward Artificial Neural Network
	// Only for prediction, using pretrained parameters
	public class MultiLayerPerceptron
	{
		private List<Layer> _layers;

		public MultiLayerPerceptron()
		{
			_layers = new List<Layer>();
		}

		public void AddLayer(Layer layer)
		{
			_layers.Add(layer);
		}

		public List<float> Compute(List<float> input)
		{
			Debug.Assert(_layers.Count <= 0 ? false : true);

			List<float> res = input;
			foreach(var layer in _layers)
			{
				res = FeedForward(res, layer);
			}

			return res;
		}

		private List<float> FeedForward(List<float> input, Layer layer)
		{
			int numNodes = layer.Biases.Count;
			float[] z = new float[numNodes];

			for(int i = 0; i < numNodes; i++)
			{
				List<float> w = layer.Weights[i];
				float bias = layer.Biases[i];

				Debug.Assert(w.Count == input.Count);

				// z = wT * x + b
				z[i] = VectorF.InnerProduct(w, input, bias);
			}

			List<float> zVector = new List<float>(z);
			float[] output = new float[numNodes];
			if(layer.Type == LayerType.SIGMOID)
			{
				for(int i = 0; i < numNodes; i++)
				{
					output[i] = 1.0f / (1.0f + (float)Math.Exp(-z[i]));
				}
			}
			else if(layer.Type == LayerType.SOFTMAX)
			{
				// Use the log-sum trick to compute the exponential sum
				// oi = exp(zi) / sum_j { exp(zj) }
				// log oi = zi - log( sum_j { exp(zj) } )
				// log oi = zi - log( sum_j { exp(zj) - m + m } )
				// log oi = zi - m - log( sum_j { exp(zj) - m } )
				float m = VectorF.MaxElement(zVector);
				float logSum = VectorF.LogSum(zVector, m);

				for(int i = 0; i < numNodes; i++)
				{
					output[i] = (float)Math.Exp(z[i] - m - logSum);
				}
			}

			return new List<float>(output);
		}

	}
}
