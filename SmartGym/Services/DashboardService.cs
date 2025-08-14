using Microsoft.EntityFrameworkCore;
using SmartGym.Data;

namespace SmartGym.Services;

public interface IDashboardService
{
    Task<List<CheckinDataPoint>> GetDailyCheckinsAsync();
    Task<List<AttendanceDataPoint>> GetDailyAttendanceAsync();
}

public class DashboardService : IDashboardService
{
    private readonly SmartGymContext _context;

    public DashboardService(SmartGymContext context)
    {
        _context = context;
    }

    public async Task<List<CheckinDataPoint>> GetDailyCheckinsAsync()
    {
        var last7Days = DateTime.Today.AddDays(-6);
        
        var checkins = await _context.Checkins!
            .Where(c => c.CheckinTime >= last7Days)
            .GroupBy(c => c.CheckinTime.Date)
            .Select(g => new CheckinDataPoint
            {
                Date = g.Key.ToString("MMM dd"),
                Count = g.Count()
            })
            .ToListAsync();

        // Fill in missing days with 0 checkins
        var allDays = new List<CheckinDataPoint>();
        for (int i = 0; i < 7; i++)
        {
            var date = last7Days.AddDays(i);
            var existing = checkins.FirstOrDefault(c => 
                c.Date == date.ToString("MMM dd"));
            allDays.Add(existing ?? new CheckinDataPoint 
            { 
                Date = date.ToString("MMM dd"), 
                Count = 0 
            });
        }

        return allDays;
    }

    public async Task<List<AttendanceDataPoint>> GetDailyAttendanceAsync()
    {
        var last7Days = DateTime.Today.AddDays(-6);
        
        // Get bookings from the last 7 days with their class session dates
        var bookings = await _context.Bookings!
            .Where(b => b.CreatedAt >= last7Days)
            .Include(b => b.ClassSession)
            .Where(b => b.ClassSession != null && b.ClassSession.SessionDateTime >= last7Days)
            .GroupBy(b => b.ClassSession!.SessionDateTime.Date)
            .Select(g => new AttendanceDataPoint
            {
                Date = g.Key.ToString("MMM dd"),
                Count = g.Count()
            })
            .ToListAsync();

        // Fill in missing days with 0 attendance
        var allDays = new List<AttendanceDataPoint>();
        for (int i = 0; i < 7; i++)
        {
            var date = last7Days.AddDays(i);
            var existing = bookings.FirstOrDefault(b => 
                b.Date == date.ToString("MMM dd"));
            allDays.Add(existing ?? new AttendanceDataPoint 
            { 
                Date = date.ToString("MMM dd"), 
                Count = 0 
            });
        }

        return allDays;
    }
}

public class CheckinDataPoint
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class AttendanceDataPoint
{
    public string Date { get; set; } = string.Empty;
    public int Count { get; set; }
}
