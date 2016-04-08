using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker;
using Newtonsoft.Json;
using Hearthstone_Deck_Tracker.Hearthstone;

namespace RankDetection
{
	public class RankDetection
	{
		private static Detection _detect;
		private static int _rank;
		private static DateTime _start;
		private static HearthstoneTextBlock _textBlock;
		private const int TIMEOUT = 30;

		public static void Load()
		{
			Logger.WriteLine("Init", "RankDetection");
			_detect = new Detection();
			AddTextBlock();
		}

		public static async void Start()
		{
			Logger.WriteLine("Rank Detection Starting", "RankDetection");
			Reset();
			while(_rank <= 0 && !IsTimedOut())
			{
				_rank = _detect.GetRank();
				await Task.Delay(1000);
			}
			Logger.WriteLine("Detection Finished: Rank=" + _rank + ", TimedOut=" + IsTimedOut(), "RankDetection");
			DisplayRank();
		}

		internal static void Reset()
		{
			Logger.WriteLine("Reset", "RankDetection");
			_rank = 0;
			_start = DateTime.Now;
			PositionTextBlock();
			DisplayRank();
		}

		private static bool IsTimedOut()
		{
			return (DateTime.Now - _start).Seconds > TIMEOUT;
		}

		private static void AddTextBlock()
		{
			var canvas = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas;

			_textBlock = new HearthstoneTextBlock();
			_textBlock.Text = "?";
			_textBlock.FontSize = 22;

			PositionTextBlock();
			canvas.Children.Add(_textBlock);
		}

		private static void PositionTextBlock()
		{
			var rect = Helper.GetHearthstoneRect(false);
			float scale = rect.Height / 1080.0f;

			int left = (int)Math.Round(94 * scale);
			int top = (int)Math.Round(976 * scale);

			Canvas.SetTop(_textBlock, top);
			Canvas.SetLeft(_textBlock, left);
		}

		internal static void RemoveTextBlock()
		{
			var canvas = Hearthstone_Deck_Tracker.API.Core.OverlayCanvas;
			canvas.Children.Remove(_textBlock);
		}

		private static void DisplayRank()
		{
			_textBlock.Text = _rank <= 0 ? "?" : _rank.ToString();
		}
	}

	class Detection
	{
		// The width / height of the rank label
		private const int RC_LABEL_WIDTH = 28;
		private const int RC_LABEL_HEIGHT = 28;

		// The position where the labels can be obtained from
		private const int RC_CAPTURE_X = 32;
		private const int RC_CAPTURE_Y = 960;
		private const int RC_CAPTURE_WIDTH = 40;
		private const int RC_CAPTURE_HEIGHT = 40;

		private const int RC_CAPTURE_SCREEN_WIDTH = 1920;
		private const int RC_CAPTURE_SCREEN_HEIGHT = 1080;

		// Ranks are displayed in white
		// Binarize images to get rid of the noise
		// Use HSV thresholds (matching pixel values are considered 1, otherwise 0)
		private const int RC_BINARIZE_MAX_SATURATION = 5;
		private const int RC_BINARIZE_MIN_VALUE = 50;

		private MultiLayerPerceptron _mlp;

		public Detection()
		{
			_mlp = new MultiLayerPerceptron();
			LoadMLP();
		}

		private void LoadMLP()
		{
			var json = Properties.Resources.rank_classifier_json;
			MLPData data = JsonConvert.DeserializeObject<MLPData>(json);
			_mlp.AddLayer(data.hidden_layer.ToLayer());
			_mlp.AddLayer(data.output_layer.ToLayer());
		}

		private int Classify(Bitmap label)
		{
			Debug.Assert(label.Width == RC_LABEL_WIDTH);
			Debug.Assert(label.Height == RC_LABEL_HEIGHT);

			List<float> input = BinarizeImageSV(label, RC_BINARIZE_MAX_SATURATION, RC_BINARIZE_MIN_VALUE);
			float sum = input.Sum();
			if(sum == 0.0f)
				return 0;

			List<float> result = _mlp.Compute(input);
			List<Tuple<int, float>> scores = new List<Tuple<int, float>>();
			for(int i = 0; i < result.Count; i++)
			{
				scores.Add(new Tuple<int, float>(i, result[i]));
			}

			scores.Sort((a, b) => b.Item2.CompareTo(a.Item2));

			//for(int i = 0; i < result.Count; i++)
			//{
				//Console.WriteLine("Rank {0} = {1}", scores[i].Item1 + 1, scores[i].Item2);
			//}

			return scores[0].Item1 + 1;
		}

		public int GetRank()
		{
			var rect = Helper.GetHearthstoneRect(false);
			var raw = Helper.CaptureHearthstone(new Point(0, 0), rect.Width, rect.Height);
			if(raw == null)
				return 0;
			//raw.Save(@"__raw__.png");
			Bitmap label = Capture(raw, RC_CAPTURE_SCREEN_WIDTH, RC_CAPTURE_SCREEN_HEIGHT, RC_CAPTURE_X, RC_CAPTURE_Y, RC_CAPTURE_WIDTH, RC_CAPTURE_HEIGHT);

			if(label.Width != RC_LABEL_WIDTH || label.Height != RC_LABEL_HEIGHT)
			{
				return 0;
			}

			return Classify(label);
		}

		public int GetRank(string filename)
		{
			Bitmap raw = new Bitmap(filename);

			Bitmap label = Capture(raw, RC_CAPTURE_SCREEN_WIDTH, RC_CAPTURE_SCREEN_HEIGHT, RC_CAPTURE_X, RC_CAPTURE_Y, RC_CAPTURE_WIDTH, RC_CAPTURE_HEIGHT);

			if(label.Width != RC_LABEL_WIDTH || label.Height != RC_LABEL_HEIGHT)
			{
				return 0;
			}

			return Classify(label);
		}

		private Bitmap Capture(Bitmap bmp, int canvasWidth, int canvasHeight, int cx, int cy, int cw, int ch)
		{
			int x, y, w, h;

			int windowHeight = bmp.Height;
			float scale = windowHeight / (float)canvasHeight;

			x = (int)Math.Round(cx * scale);
			y = (int)Math.Round(cy * scale);

			w = (int)Math.Round(cw * scale);
			h = (int)Math.Round(ch * scale);

			Bitmap capture = new Bitmap(RC_LABEL_WIDTH, RC_LABEL_HEIGHT);
			Graphics graphic = Graphics.FromImage(capture);
			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.SmoothingMode = SmoothingMode.HighQuality;
			graphic.DrawImage(bmp,
				new Rectangle(0, 0, RC_LABEL_WIDTH, RC_LABEL_HEIGHT),
				new Rectangle(x, y, w, h),
				GraphicsUnit.Pixel);

			graphic.Dispose();

			//capture.Save(@"__capped__.png");
			return capture;
		}

		private List<float> BinarizeImageSV(Bitmap img, float maxSaturation, float minValue)
		{
			Bitmap bw = new Bitmap(img.Width, img.Height);

			float[] vOut = new float[img.Width * img.Height];

			for(int y = 0; y < img.Height; y++)
			{
				for(int x = 0; x < img.Width; x++)
				{
					int idx = y * img.Width + x;

					Color pixel = img.GetPixel(x, y);

					// GetHsv
					int s = (int)Math.Round(pixel.GetSaturation() * 255);
					int v = (int)Math.Round(pixel.GetBrightness() * 255); // TODO: is this HSV or HSL?
					//Console.WriteLine("{2} {0} {1}", s, v, pixel.GetHue());
					bool white = (s <= maxSaturation && v >= minValue);
					vOut[idx] = white ? 1.0f : 0.0f;
					bw.SetPixel(x, y, white ? Color.White : Color.Black);
				}
			}
			//bw.Save(@"__bw__.png");

			return new List<float>(vOut);
		}
	}

	class MLPData
	{
		public MLPRecord hidden_layer { get; set; }
		public MLPRecord output_layer { get; set; }
	}

	class MLPRecord
	{
		public List<List<float>> weights { get; set; }
		public string type { get; set; }
		public List<float> biases { get; set; }
		public int num_nodes { get; set; }

		public Layer ToLayer()
		{
			Layer layer = new Layer();
			layer.Biases = biases;
			layer.Weights = new Matrix<float>(weights);
			layer.Type = LayerType.SIGMOID;
			if(type.ToLower() == "softmax")
			{
				layer.Type = LayerType.SOFTMAX;
			}
			return layer;
		}

		public override string ToString()
		{
			return String.Format("{0} - {1} ({2}, {3})", type, num_nodes, weights.Count, biases.Count);
		}
	}
}
