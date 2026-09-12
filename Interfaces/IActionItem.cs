using System;
using Task.Models;

namespace Task.Interfaces
{
    public interface IActionItem
    {
        string Name { get; }
        int Duration { get; set; }
        TimeOnly StartTime { get; set; }
        ActionType Type { get; }
    }
}