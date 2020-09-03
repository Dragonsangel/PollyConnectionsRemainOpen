using System;
using System.Threading.Tasks;

namespace PollyConnectionsRemainOpen
{
	public interface ITestProcessor
	{
		Task<Boolean> PerformCall(Boolean withPolly);
	}
}