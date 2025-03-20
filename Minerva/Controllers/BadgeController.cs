using Badger;
using Microsoft.AspNetCore.Mvc;

namespace Minerva.Controllers
{
    public class BadgeController : Controller
    {
        private readonly ILogger<BadgeController> _logger;

        public BadgeController(ILogger<BadgeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("habbo-imaging/badge/{badgeCode}")]
        public IActionResult BadgeImager(string badgeCode)
        {
            if (badgeCode != null && badgeCode.Length > 0)
            {
                var badge = Badge.ParseBadgeData(new BadgeSettings
                {
                    IsShockwaveBadge = Program.SHOCKWAVE_BADGE_RENDER
                }, badgeCode);

                //var avatar = new Avatar(figure, size, bodyDirection, headDirection, figuredataReader, action: action, gesture: gesture, headOnly: headOnly, frame: frame, carryDrink: carryDrink, cropImage: cropImage);

                if (badgeCode.EndsWith(".gif"))
                {
                    var badgeData = badge.Render(gifEncoder: true);

                    if (badgeData != null)
                        return File(badgeData, "image/gif");
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
                /*var badge = GetFromServer.ParseBadgeData(badgeCode);

                badge.Parts.ForEach(x =>
                {
                    x.IsShockwaveBadge = Program.SHOCKWAVE_BADGE_RENDER;
                });*/

                var badge = Badge.ParseBadgeData(new BadgeSettings
                {
                    IsShockwaveBadge = Program.SHOCKWAVE_BADGE_RENDER
                }, badgeCode);

                //var avatar = new Avatar(figure, size, bodyDirection, headDirection, figuredataReader, action: action, gesture: gesture, headOnly: headOnly, frame: frame, carryDrink: carryDrink, cropImage: cropImage);

                var badgeData = badge.Render(gifEncoder: true, forceWhiteBackground: true);

                if (badgeData != null)
                    return File(badgeData, "image/gif");
            }

            return StatusCode(403);
        }
    }
}
