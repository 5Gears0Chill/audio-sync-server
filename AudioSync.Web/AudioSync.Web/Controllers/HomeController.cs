using AudioSync.Core.DataAccess.Entities;
using AudioSync.Core.DataAccess.Models;
using AudioSync.Core.Interfaces.DataAccess;
using AudioSync.Core.Interfaces.Repositories;
using AudioSync.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AudioSync.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUnitOfWorkManager unitOfWorkManager)
        {
            _logger = logger;
            _unitOfWorkManager = unitOfWorkManager;
        }

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        

        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkManager.ExecuteSingleAsync<IDeviceRepository, DataResult<Device>>(x => x.SaveDeviceAsync(new Device
                        {
                            DeviceId = "A New Random String",

                        }));

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
