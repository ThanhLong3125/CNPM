using backend.DTOs;
using backend.Services.BackgroundServices;

namespace backend.Services
{
    public static class NotificationHelper
    {
        // System notification
        public static void SendSystemNotification(
            this IServiceProvider services,
            Guid userId,
            string message)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = message
            };

            services.QueueNotification(notification);
        }

        // AI Analysis Result notification
        public static void SendAIAnalysisResultNotification(
            this IServiceProvider services,
            Guid userId,
            string imageId,
            string diagnosisResult)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = $"AI analysis for image {imageId} is complete. Result: {diagnosisResult}"
            };

            services.QueueNotification(notification);
        }

        // New Assignment notification
        public static void SendNewAssignmentNotification(
            this IServiceProvider services,
            Guid userId,
            string patientName,
            string assignmentType)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = $"You have been assigned to {assignmentType} for patient {patientName}"
            };

            services.QueueNotification(notification);
        }

        // Patient Update notification
        public static void SendPatientUpdateNotification(
            this IServiceProvider services,
            Guid userId,
            string patientName,
            string updateType)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = $"Patient {patientName} has a new {updateType}"
            };

            services.QueueNotification(notification);
        }

        // Image Upload notification
        public static void SendImageUploadedNotification(
            this IServiceProvider services,
            Guid userId,
            string patientName,
            string imageType)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = $"A new {imageType} image for patient {patientName} has been uploaded"
            };

            services.QueueNotification(notification);
        }

        // Reminder notification
        public static void SendReminderNotification(
            this IServiceProvider services,
            Guid userId,
            string reminderType,
            string reminderDetails)
        {
            var notification = new CreateNotificationDto
            {
                UserId = userId,
                Message = $"{reminderType} Reminder: {reminderDetails}"
            };

            services.QueueNotification(notification);
        }
    }
}
