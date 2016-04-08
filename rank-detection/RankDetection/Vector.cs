using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankDetection
{
	public static class VectorF
	{
		public static float InnerProduct(List<float> a, List<float> b, float init)
		{
			if(b.Count < a.Count)
			{
				throw new InvalidOperationException("InnerProduct second vector is not long enough");
			}
			var product = init;

			for(int i = 0; i < a.Count; i++)
			{
				product += a[i] * b[i];
			}

			return product;
		}

		public static float MaxElement(List<float> vector)
		{
			float max = float.NaN;
			foreach(var v in vector)
			{
				if(float.IsNaN(max) || v > max)
				{
					max = v;
				}
			}
			return max;
		}

		public static float LogSum(List<float> vector, float f)
		{
			float sum = 0;
			foreach(var v in vector)
			{
				sum += (float)Math.Exp(v - f);
			}
			return (float)Math.Log(sum);
		}
	}

	public class Matrix<T>
	{
		private List<List<T>> _matrix;

		public Matrix()
		{
			_matrix = new List<List<T>>();
		}

		public Matrix(List<List<T>> list)
		{
			_matrix = list;
		}

		public List<T> this[int i]
		{
			get { return _matrix[i]; }
			set { _matrix[i] = value; }
		}

		public int Size
		{
			get { return _matrix.Count; }
		}
	}
}
