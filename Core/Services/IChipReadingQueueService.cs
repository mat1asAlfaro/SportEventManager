using SportEventManager.DTOs;

namespace SportEventManager.Core.Services;

public interface IChipReadingQueueService
{
    ValueTask EnqueueAsync(ChipReadingDTO reading);
    int GetQueueCount();
}
