using System.Threading.Tasks;

namespace SDIKit.Common.Interfaces
{
    public interface IStartupTask
    {
        int Order { get; set; }

        Task InvokeAsync();
    }
}