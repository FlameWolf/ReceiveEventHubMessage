using System.Text;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ReceiveEventHubMessage;

public class ReceiveEventHubMessageFunc
{
	private readonly ILogger<ReceiveEventHubMessageFunc> _logger;

	public ReceiveEventHubMessageFunc(ILogger<ReceiveEventHubMessageFunc> logger)
	{
		_logger = logger;
	}

	[Function(nameof(ReceiveEventHubMessageFunc))]
	public void Run([EventHubTrigger("test-event-hub", Connection = "EventHubConnectionAppSetting")] EventData[] events)
	{
		foreach (EventData @event in events)
		{
			_logger.LogInformation("Event Body: {body}", Encoding.UTF8.GetString(@event.Body.ToArray()));
			_logger.LogInformation("Event Content-Type: {contentType}", @event.ContentType);
		}
	}
}
