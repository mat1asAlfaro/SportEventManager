using SportEventManager.DTOs;

namespace SportEventManager.Services;

public interface IChipReadingQueueService
{
    ValueTask EnqueueAsync(ChipReadingDTO reading);
    int GetQueueCount();
}
