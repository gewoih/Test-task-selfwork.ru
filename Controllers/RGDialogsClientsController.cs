using Microsoft.AspNetCore.Mvc;
using Test_task_selfwork.ru.Models;

namespace Test_task_selfwork.ru.Controllers
{
	[Controller]
	[Route("dialogs")]
	public class RGDialogsClientsController : Controller
	{
		[HttpGet]
        [Route("findDialogGuidByClients")]
		public Guid FindDialogGuidByClients(List<Guid> clientsGuids)
		{
			List<RGDialogsClients> dialogs = new RGDialogsClients().Init();

			var dialogGuids = dialogs.Select(d => d.IDRGDialog).Distinct();

			foreach (var dialogGuid in dialogGuids)
			{
				IEnumerable<Guid> dialogClientsGuids = dialogs
														.Where(d => d.IDRGDialog == dialogGuid)
														.Select(d => d.IDClient);

				int count = clientsGuids.Except(dialogClientsGuids).Count();

				if (count == 0)
					return dialogGuid;
			}

			return Guid.Empty;
		}
	}
}
