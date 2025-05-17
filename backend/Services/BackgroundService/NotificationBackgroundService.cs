using backend.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace backend.Services.BackgroundServices
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConcurrentQueue<CreateNotificationDto> _notificationQueue;
        private readonly ILogger<NotificationBackgroundService> _logger;

        public NotificationBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<NotificationBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _notificationQueue = new ConcurrentQueue<CreateNotificationDto>();
            _logger = logger;
        }

        public void QueueNotification(CreateNotificationDto notification)
        {
            _notificationQueue.Enqueue(notification);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessQueuedNotifications(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while processing notifications.");
                }

                // Wait before processing the next batch
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Notification Background Service is stopping.");
        }

        private async Task ProcessQueuedNotifications(CancellationToken stoppingToken)
        {
            if (_notificationQueue.IsEmpty)
            {
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            // Process up to 20 notifications at a time
            var processCount = 0;
            var maxProcessCount = 20;

            while (processCount < maxProcessCount && _notificationQueue.TryDequeue(out var notification))
            {
                try
                {
                    await notificationService.CreateNotificationAsync(notification);
                    processCount++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating notification: {Message}", notification.Message);
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            if (processCount > 0)
            {
                _logger.LogInformation("Processed {Count} notifications", processCount);
            }
        }
    }

    // Extension method to make it easier to queue notifications from anywhere in the application
    public static class NotificationBackgroundServiceExtensions
    {
        public static void QueueNotification(this IServiceProvider services, CreateNotificationDto notification)
        {
            var backgroundService = services.GetService<NotificationBackgroundService>();
            backgroundService?.QueueNotification(notification);
        }
    }
}
