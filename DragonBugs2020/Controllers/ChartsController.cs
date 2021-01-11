using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonBugs2020.Data;
using DragonBugs2020.Models.ChartModels;
using Microsoft.AspNetCore.Mvc;

namespace DragonBugs2020.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ChartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult PriorityDonutChart()
        {
            List<DonutChartData> result = new List<DonutChartData>();

            var ticketPriorities = _context.TicketPriorities.ToList();

            foreach (var priority in ticketPriorities)
            {
                result.Add(new DonutChartData
                {
                    Label = priority.Name,
                    Value = _context.Tickets.Where(t => t.TicketPriorityId == priority.Id).Count()
                });
            }
            return Json(result);
        }

        public JsonResult StatusDonutChart()
        {
            List<DonutChartData> result = new List<DonutChartData>();

            var ticketStatuses = _context.TicketStatuses.ToList();

            foreach (var status in ticketStatuses)
            {
                result.Add(new DonutChartData
                {
                    Label = status.Name,
                    Value = _context.Tickets.Where(t => t.TicketStatusId == status.Id).Count()
                });
            }
            return Json(result);
        }

        public JsonResult TypeDonutChart()
        {
            List<DonutChartData> result = new List<DonutChartData>();

            var ticketTypes = _context.TicketTypes.ToList();

            foreach (var type in ticketTypes)
            {
                result.Add(new DonutChartData
                {
                    Label = type.Name,
                    Value = _context.Tickets.Where(t => t.TicketTypeId == type.Id).Count()
                });
            }
            return Json(result);
        }

        //public JsonResult ProjectDetailsPriorityChart()
        //{
        //    List<DonutChartData> result = new List<DonutChartData>();

        //    var ticketPriorities = _context.TicketPriorities.ToList();
        //    var projectId = 

        //    foreach (var priority in ticketPriorities)
        //    {
        //        result.Add(new DonutChartData
        //        {
        //            Label = priority.Name,
        //            Value = _context.Tickets.Where(t => t.TicketPriorityId == priority.Id)
        //            .Where(p => p.ProjectId == )
        //            .Count()
        //        });
        //    }
        //    return Json(result);
        //}

        public JsonResult ProjectDetailsStatusChart()
        {
            List<DonutChartData> result = new List<DonutChartData>();

            var ticketStatuses = _context.TicketStatuses.ToList();

            foreach (var status in ticketStatuses)
            {
                result.Add(new DonutChartData
                {
                    Label = status.Name,
                    Value = _context.Tickets.Where(t => t.TicketStatusId == status.Id).Count()
                });
            }
            return Json(result);
        }

        public JsonResult ProjectDetailsTypeChart()
        {
            List<DonutChartData> result = new List<DonutChartData>();

            var ticketTypes = _context.TicketTypes.ToList();

            foreach (var type in ticketTypes)
            {
                result.Add(new DonutChartData
                {
                    Label = type.Name,
                    Value = _context.Tickets.Where(t => t.TicketTypeId == type.Id).Count()
                });
            }
            return Json(result);
        }
    }
}
