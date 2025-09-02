using Alcosmos.Figure;
using Avatara;
using Avatara.Extensions;
using Avatara.Figure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Minerva.Controllers
{
    public class FigureController : Controller
    {
        private readonly ILogger<FigureController> _logger;

        public FigureController(ILogger<FigureController> logger)
        {
            _logger = logger;
        }

        [HttpGet("habbo-imaging/avatarimage/convert")]
        public IActionResult Convert()
        {
            string? figure = null;

            if (Request.Query.ContainsKey("figure"))
            {
                Request.Query.TryGetValue("figure", out var value);
                figure = value.ToString();
            }

            try
            {
                return Json(new { figure = FigureConverter.Instance.ConvertOldToNew(figure) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during conversion");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("habbo-imaging/avatarimage")]
        public IActionResult Index()
        {
            string size = "b";
            int bodyDirection = 2;
            int headDirection = 2;
            string? figure = null;
            string action = "std";
            string gesture = "std";
            bool headOnly = false;
            int frame = 1;
            int carryDrink = -1;
            bool cropImage = false;

            if (Request.Query.ContainsKey("figure"))
            {
                Request.Query.TryGetValue("figure", out var value);
                figure = value.ToString();
            }

            if (Request.Query.ContainsKey("action"))
            {
                Request.Query.TryGetValue("action", out var value);
                action = value.ToString();
            }

            if (Request.Query.ContainsKey("gesture"))
            {
                Request.Query.TryGetValue("gesture", out var value);
                gesture = value.ToString();
            }

            if (Request.Query.ContainsKey("figure"))
            {
                Request.Query.TryGetValue("figure", out var value);
                figure = value.ToString();
            }

            if (Request.Query.ContainsKey("size"))
            {
                Request.Query.TryGetValue("size", out var value);
                size = value.ToString();
            }

            if (Request.Query.ContainsKey("head"))
            {
                Request.Query.TryGetValue("head", out var value);
                headOnly = value.ToString() == "1" || value.ToString() == "true";
            }

            if (Request.Query.ContainsKey("direction"))
            {
                Request.Query.TryGetValue("direction", out var value);

                if (value.ToString().IsNumeric())
                {
                    bodyDirection = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("head_direction"))
            {
                Request.Query.TryGetValue("head_direction", out var value);

                if (value.ToString().IsNumeric())
                {
                    headDirection = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("frame"))
            {
                Request.Query.TryGetValue("frame", out var value);

                if (value.ToString().IsNumeric())
                {
                    int v = int.Parse(value.ToString());
                    frame = v < 1 ? 1 : v;
                }
            }

            if (Request.Query.ContainsKey("drk"))
            {
                Request.Query.TryGetValue("drk", out var value);
                action = (value.ToString() == "1" || value.ToString() == "true") ? "drk" : action;
            }

            if (Request.Query.ContainsKey("crop"))
            {
                Request.Query.TryGetValue("crop", out var value);
                cropImage = (value.ToString() == "1" || value.ToString() == "true");
            }

            if (Request.Query.ContainsKey("crr"))
            {
                Request.Query.TryGetValue("crr", out var value);

                if (value.ToString().IsNumeric())
                {
                    carryDrink = int.Parse(value.ToString());
                }
            }

            if (figure != null && figure.Length > 0)
            {
                var avatar = new Avatar(FiguredataReader.Instance, figure, size, bodyDirection, headDirection, action: action, gesture: gesture, headOnly: headOnly, frame: frame, carryDrink: carryDrink, cropImage: cropImage);
                var figureData = avatar.Run();

                return File(figureData, "image/png");
            }

            return StatusCode(403);
        }
    }
}