using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using back_end_net_core_5.BusinessLogic;
using back_end_net_core_5.ViewModel;

namespace back_end_net_core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwsController : ControllerBase
    {
        IUploadBucketBll _uploadBucket;
        public AwsController(IUploadBucketBll uploadBucket)
        {
            _uploadBucket = uploadBucket;
        }

        [HttpPost]
        public ActionResult Post([FromBody] LoginVM objeto)
        {

            return Ok();
        }
    }
}
