using System.Threading.Channels;
using SportEventManager.DTOs;
using SportEventManager.Core;

namespace SportEventManager.Services;

public class ChipReadingQueueService : BackgroundService, IChipReadingQueueService
{
    private readonly Channel<ChipReadingDTO> _queue;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ChipReadingQueueService> _logger;

    public ChipReadingQueueService(
        IServiceProvider serviceProvider,
        ILogger<ChipReadingQueueService> logger)
    {
        var options = new BoundedChannelOptions(1000) // Max tokens in queue
        {
            FullMode = BoundedChannelFullMode.Wait // Wait if the queue is full
        };
        _queue = Channel.CreateBounded<ChipReadingDTO>(options);
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async ValueTask EnqueueAsync(ChipReadingDTO reading)
    {
        await _queue.Writer.WriteAsync(reading);
        _logger.LogInformation($"Chip reading enqueued: ChipId {reading.ChipId}, SplitId {reading.SplitId}");
    }

    public int GetQueueCount()
    {
        return _queue.Reader.Count;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Chip Reading Queue Service started");

        await foreach (var reading in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var timeRecordRepository = scope.ServiceProvider.GetRequiredService<ITimeRecordRepository>();

                var result = await timeRecordRepository.RegisterChipReadingAsync(reading);

                if (result != null)
                {
                    _logger.LogInformation($"Successfully processed chip reading from queue: {result.TimeRecordId}");
                }
                else
                {
                    _logger.LogWarning($"Failed to process chip reading from queue: ChipId {reading.ChipId}, SplitId {reading.SplitId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing chip reading from queue: ChipId {reading.ChipId}, SplitId {reading.SplitId}");
            }
        }

        _logger.LogInformation("Chip Reading Queue Service stopped");
    }
}
