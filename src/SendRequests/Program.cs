using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SendRequests
{
    public class Program
    {

        static ConcurrentQueue<int> queue;
        public static void Main(string[] args)
        {
            queue = new ConcurrentQueue<int>();

            var tasks = new List<Task>();

            for (int x = 0; x < 1000; x++)
            {
                Console.WriteLine($"Sent {x}");
                queue.Enqueue(x);
            }

            for (int x = 1; x <= 8; x++)
            {

                tasks.Add(Task.Run(async () =>
                {
                    int id = 0;

                    while (queue.TryDequeue(out id))
                    {
                        Console.WriteLine($"Processing {id}");
                        await GetBlogAsync(id);
                    }
                }));

            }

            Task.WaitAll(tasks.ToArray());
        }
        private static async Task GetBlogAsync(int id)
        {
            HttpClient client = new HttpClient();

            try
            {
                await client.GetStringAsync($"http://localhost:5000/api/blogs/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {id}");
            }
            Console.WriteLine($"Processed {id}");

        }
    }
}
