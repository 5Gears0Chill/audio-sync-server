using AudioSync.Core.DataAccess.Entities;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.DataAccess;
using AudioSync.Core.Interfaces.Repositories;
using AudioSync.Web.Hubs;
using AudioSync.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AudioSync.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private member variable(s)
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IHubContext<ConnectionHub> _hubContext;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, IUnitOfWorkManager unitOfWorkManager, IHubContext<ConnectionHub> hubContext)
        {
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
            _hubContext = hubContext;
        }
        #endregion


        public async Task<IActionResult> Index()
        {
            #region To Discard
            /*
            var result = await _unitOfWorkManager.ExecuteSingleAsync<IDeviceRepository, DataResult<Device>>(x => x.SaveDeviceAsync(new Device
            {
                DeviceId = "A New Random String",

            }));
            */
            #endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
