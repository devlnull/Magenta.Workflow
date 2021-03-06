﻿using System;
using System.Threading.Tasks;
using Magenta.Workflow;
using Magenta.Workflow.UseCases.InitFlowState;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class FlowStatesController : BaseController
    {
        public FlowStatesController(IFlowManager flowManager, IFlowReportManager flowReportManager) 
            : base(flowManager, flowReportManager)
        {
        }

        public async Task<IActionResult> Index(Guid flowTypeId)
        {
            var states = await FlowReportManager.GetStatesByTypeIdAsync(flowTypeId);
            return View("Index", states.Result);
        }

        [HttpGet]
        public IActionResult Create(Guid flowTypeId)
        {
            return View("Create", new InitFlowStateModel()
            {
                TypeId = flowTypeId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(InitFlowStateModel model)
        {
            var result = await FlowManager.InitFlowStateAsync(model);
            HandleFlowResult(result);
            return View("Create", new InitFlowStateModel()
            {
                TypeId = model.TypeId
            });
        }
    }
}
