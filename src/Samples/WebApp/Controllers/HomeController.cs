using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.UseCases.InitFlowType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFlowManager _flowManager;
        private readonly IFlowReportManager _flowReportManager;

        public HomeController(
            IFlowManager flowManager,
            IFlowReportManager flowReportManager,
            ILogger<HomeController> logger)
        {
            _flowManager = flowManager ?? throw new ArgumentNullException(nameof(flowManager));
            _flowReportManager = flowReportManager ?? throw new ArgumentNullException(nameof(flowReportManager));

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _flowReportManager.GetTypesAsync();

            TempData["message"] = SerializeData(items);
            return View();
        }

        private string SerializeData(object obj)
        {
            var serialized = JsonSerializer.Serialize(obj,
                options: new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    IgnoreNullValues = true,
                    MaxDepth = 100
                });
            return serialized;
        }

        public async Task<IActionResult> Privacy()
        {
            var createFlowType = await _flowManager.InitFlowTypeAsync(
                new InitFlowTypeModel()
                {
                    EntityType = typeof(HomeController),
                    EntityPayloadType = typeof(HomeController),
                    Name = $"test-{Guid.NewGuid()}",
                });

            TempData["message"] = SerializeData(createFlowType);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
