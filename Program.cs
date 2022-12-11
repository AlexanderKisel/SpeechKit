using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace TTS
{
  class Program
  {
    
    static void Main()
    {
      string confirm;
      do {
        Console.WriteLine("Желаете получить талон? (yes/no)");
        confirm = Console.ReadLine().ToString();
        if (confirm == "no") {break;}
          Tts().GetAwaiter().GetResult();
          File.Open(num + ".mp3",FileMode.Open);
          }while (confirm == "yes");
    }
      static int num = 0;
    static async Task Tts()
    {
      const string iamToken = "t1.9euelZrNjsaalJiKiZmQy46Yi4qJnu3rnpWayIyejZDPmM7MxsaWko7Mzczl8_cEJilj-e93P2ph_t3z90RUJmP573c_amH-.yxivQe8TpdhqbhidgMeTqgSWunyT8aLeJLUqgDbizNKOWXaJLQ6Vd8uraDe3kaxGhVJzt2iOwWKUJz1wcNf_DQ"; // Укажите IAM-токен.
      const string folderId = "b1g51lmivsu01gtp47eh"; // Укажите идентификатор каталога.
      Random rnd  = new Random();
      int window = rnd.Next(1,8);
      char letter = (new char[] {'А','Б','В','Г'})[rnd.Next(4)];
      num++;
      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Authorization", "Bearer " + iamToken);
      var values = new Dictionary<string, string>
      {
        { "text", "Талон номер" + letter + "sil<[10]>" + num + "sil<[10]>" + "окно номер" + window },
        { "lang", "ru-RU" },
        { "voice", "filipp" },
        { "folderId", folderId }
      };
      var content = new FormUrlEncodedContent(values);
      var response = await client.PostAsync("https://tts.api.cloud.yandex.net/speech/v1/tts:synthesize", content);
      var responseBytes = await response.Content.ReadAsByteArrayAsync();
      File.WriteAllBytes(num + ".mp3", responseBytes);
    }
  }
}