using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        List<Task> tasks = new List<Task>();
        SemaphoreSlim semaphore = new SemaphoreSlim(3); // Allows 3 tasks at a time

        for (int i = 0; i < 50; i++)
        {
            int taskId = i; // To avoid closure issues
            tasks.Add(Task.Run(async () =>
            {
                await semaphore.WaitAsync(); // Wait for available slot
                try
                {
                    await RunTaskAsync(taskId);
                }
                finally
                {
                    semaphore.Release(); // Release the slot after task completion
                }
            }));
        }

        await Task.WhenAll(tasks); // Wait for all tasks to complete
    }

    static async Task RunTaskAsync(int taskId)
    {
        Console.WriteLine($"Task {taskId} is starting.");
        await Task.Delay(1000); // Simulate work with 1-second delay
        Console.WriteLine($"Task {taskId} is done.");
    }
}
