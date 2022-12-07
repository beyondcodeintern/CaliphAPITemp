using System.Web.Http;

namespace Caliph.API.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
    }
}