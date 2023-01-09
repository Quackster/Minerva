using Avatara;
using Avatara.Extensions;
using Avatara.Figure;
using Badger;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Helios.Imager.Controllers
{
    public class BadgeController : Controller
    {
        private readonly ILogger<BadgeController> _logger;

        public BadgeController(ILogger<BadgeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("habbo-imaging/badge/{badgeCode}")]
        public IActionResult Badge(string badgeCode)
        {
            if (badgeCode != null && badgeCode.Length > 0)
            {
                var badge = GetFromServer.ParseBadgeData(badgeCode);
                //var avatar = new Avatar(figure, size, bodyDirection, headDirection, figuredataReader, action: action, gesture: gesture, headOnly: headOnly, frame: frame, carryDrink: carryDrink, cropImage: cropImage);

                if (badgeCode.EndsWith(".gif"))
                {
                    var badgeData = badge.Render(gifEncoder: true);

                    if (badgeData != null)
                        return File(badgeData, "image/png");
                }
                else
                {
                    var badgeData = badge.Render(gifEncoder: false);

                    if (badgeData != null)
                        return File(badgeData, "image/png");
                }
            }

            return StatusCode(403);
        }

        [HttpGet("habbo-imaging/badge-fill/{badgeCode}")]
        public IActionResult BadgeFill(string badgeCode)
        {
            if (badgeCode != null && badgeCode.Length > 0)
            {
                var badge = GetFromServer.ParseBadgeData(badgeCode);
                //var avatar = new Avatar(figure, size, bodyDirection, headDirection, figuredataReader, action: action, gesture: gesture, headOnly: headOnly, frame: frame, carryDrink: carryDrink, cropImage: cropImage);
                
                if (badgeCode.EndsWith(".gif"))
                {
                    var badgeData = badge.Render(gifEncoder: true);

                    if (badgeData != null)
                        return File(badgeData, "image/png");
                }
                else
                {
                    var badgeData = badge.Render(gifEncoder: false);

                    if (badgeData != null)
                        return File(badgeData, "image/png");
                }
            }

            return StatusCode(403);
        }
    }
}