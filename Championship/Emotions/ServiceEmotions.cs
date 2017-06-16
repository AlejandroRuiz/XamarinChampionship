using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;

namespace Emotions
{
	public class ServiceEmotions
	{
		static string key = "a0f2543bc59e4478ab34f5c1e9f245f3";
		public static async Task<Dictionary<string, float>> GetEmotions(Stream
	stream)
		{
			EmotionServiceClient cliente = new EmotionServiceClient(key);
			var emotions = await cliente.RecognizeAsync(stream);
			if (emotions == null || emotions.Count() == 0)
				return null;
			return emotions[0].Scores.ToRankedList().ToDictionary(x => x.Key, x => x.Value);
		}
	}
}
