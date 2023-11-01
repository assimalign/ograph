using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertySetter
{
    Task InvokeAsync(object instance, object value);
}
