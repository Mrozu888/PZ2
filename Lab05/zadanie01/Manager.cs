class Manager
{
    private readonly List<Thread> producers = new List<Thread>();
    private readonly List<Thread> consumers = new List<Thread>();
    private readonly Queue<Data> queue = new Queue<Data>();
    private readonly object queueLock = new object();
    private readonly int maxDelay;
    private volatile bool isRunning = false;
    private readonly List<Dictionary<int, int>> consumerStats;

    public Manager(int producerCount, int consumerCount, int maxDelay)
    {
        this.maxDelay = maxDelay;
        consumerStats = new List<Dictionary<int, int>>(consumerCount);

        for (int i = 0; i < producerCount; i++)
        {
            int producerId = i;
            producers.Add(new Thread(() => ProducerWork(producerId)) { IsBackground = false });
        }

        for (int i = 0; i < consumerCount; i++)
        {
            int consumerId = i;
            var stats = new Dictionary<int, int>();
            consumerStats.Add(stats);
            consumers.Add(new Thread(() => ConsumerWork(consumerId, stats)) { IsBackground = false });
        }
    }

    public void Start()
    {
        isRunning = true;
        producers.ForEach(p => p.Start());
        consumers.ForEach(c => c.Start());
    }

    public void Stop()
    {
        isRunning = false;
        
        // Poczekaj na zakończenie wątków producentów
        foreach (var producer in producers)
        {
            producer.Join();
        }

        // Poczekaj na zakończenie wątków konsumentów
        foreach (var consumer in consumers)
        {
            consumer.Join();
        }

        // Wyświetl statystyki
        for (int i = 0; i < consumerStats.Count; i++)
        {
            Console.WriteLine($"Konsument {i} skonsumował:");
            foreach (var stat in consumerStats[i])
            {
                Console.WriteLine($"  Producent {stat.Key} - {stat.Value}");
            }
        }
    }

    private void ProducerWork(int producerId)
    {
        Random rnd = new Random(producerId + Environment.TickCount);
        
        while (isRunning)
        {
            int delay = rnd.Next(maxDelay);
            Thread.Sleep(delay);

            var data = new Data(producerId);

            lock (queueLock)
            {
                queue.Enqueue(data);
                Monitor.Pulse(queueLock);
            }

            Console.WriteLine($"Producent {producerId} wyprodukował dane. Rozmiar kolejki: {queue.Count}");
        }

        Console.WriteLine($"Producent {producerId} zakończył pracę.");
    }

    private void ConsumerWork(int consumerId, Dictionary<int, int> stats)
    {
        Random rnd = new Random(consumerId + Environment.TickCount);
        
        while (isRunning)
        {
            int delay = rnd.Next(maxDelay);
            Thread.Sleep(delay);

            Data data = null;

            lock (queueLock)
            {
                while (queue.Count == 0 && isRunning)
                {
                    Monitor.Wait(queueLock, 1000);
                }

                if (queue.Count > 0)
                {
                    data = queue.Dequeue();
                }
            }

            if (data != null)
            {
                if (!stats.ContainsKey(data.ProducerId))
                {
                    stats[data.ProducerId] = 0;
                }
                stats[data.ProducerId]++;

                Console.WriteLine($"Konsument {consumerId} skonsumował dane od producenta {data.ProducerId}. Rozmiar kolejki: {queue.Count}");
            }
        }

        Console.WriteLine($"Konsument {consumerId} zakończył pracę.");
    }
}