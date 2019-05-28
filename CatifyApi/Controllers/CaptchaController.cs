using CatifyApi.Captcha;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CatifyApi.Controllers
{
    [Route("api/[controller]")]
    public class CaptchaController : Controller
    {
        [HttpPost]
        public async Task<string> Post([FromBody]string value)
        {
            var captchaSolver = new CaptchaSolver();
            return await captchaSolver.Solve(value);
        }
    }
}
