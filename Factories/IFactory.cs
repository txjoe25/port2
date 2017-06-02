using net_core_mvc.Models;

namespace net_core_mvc.Factories
{
    public interface IFactory<T> where T : BaseEntity {}
}