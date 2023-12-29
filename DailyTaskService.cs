
using System.Threading;

namespace WebApplication1
{
    public class DailyTaskService : BackgroundService
    {
        private readonly ILogger<DailyTaskService> _logger;
        public DailyTaskService(ILogger<DailyTaskService> logger)
        {
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (true)
            {
                var now = DateTime.Now;
                var scheduledTime = new DateTime(now.Year, now.Month, now.Day,now.Hour,0, 10); // 12:00 AM
                if (now > scheduledTime)
                {
                    scheduledTime = scheduledTime.AddDays(1);
                }

                TimeSpan timeToGo = scheduledTime - now;
                if (timeToGo <= TimeSpan.Zero)
                {
                    timeToGo = TimeSpan.Zero;
                }
                _logger.LogInformation($"Waiting for {timeToGo} before executing DeleteTenant.");
                //await Task.Delay(timeToGo);
               await Task.Delay(timeToGo).ContinueWith(t =>
                {
                    var now = DateTime.Now;
                    _logger.LogInformation($"DeleteTenant executed at {now}");
                    Console.WriteLine("sadsf" + now);

                }, stoppingToken);
               
            }
        }
       //public async Task DeleteTenant()
       // {
       //     var now = DateTime.Now;
       //     _logger.LogInformation($"DeleteTenant executed at {now}");
       //     Console.WriteLine("sadsf"+ now);
        
       // }
    }
}

//using (Timer timer = new Timer(async _ =>
//{
//    try
//    {
//        await DeleteTenant();
//        _logger.LogInformation("DeleteTenant executed successfully.");
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex, "Error executing DeleteTenant.");
//    }
//}, null, timeToGo, TimeSpan.FromSeconds(5))) 
//{
//    await Task.Delay(Timeout.Infinite);
//}

// await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
// Set the timer to run every day at 12:00 AM
//  Timer _timer = new Timer (DeleteTenant, null, timeToGo, TimeSpan.FromSeconds(2));
//await Task.Delay(timeToGo);



//private readonly List<Timer> timers = new List<Timer>();
//private readonly object lockObject = new object();

//public void ScheduleTask(int hour, int min, double intervalInHour, Action task)
//{
//    DateTime now = DateTime.Now;
//    DateTime firstRun = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);

//    if (now > firstRun)
//    {
//        firstRun = firstRun.AddDays(1);
//    }

//    TimeSpan timeToGo = firstRun - now;
//    if (timeToGo <= TimeSpan.Zero)
//    {
//        timeToGo = TimeSpan.Zero;
//    }

//    var timer = new Timer(_ =>
//    {
//        try
//        {
//            task.Invoke();
//        }
//        catch (Exception ex)
//        {
//            // Handle exceptions, log, or take appropriate action
//        }
//    }, null, timeToGo, TimeSpan.FromHours(intervalInHour));

//    lock (lockObject)
//    {
//        timers.Add(timer);
//    }
//}

//public void DisposeTimers()
//{
//    lock (lockObject)
//    {
//        foreach (var timer in timers)
//        {
//            timer.Dispose();
//        }

//        timers.Clear();
//    }
//}