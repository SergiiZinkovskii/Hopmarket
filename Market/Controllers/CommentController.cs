
using Market.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Market.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }



        // GET: /Comment/GetComments/{productId}
        [HttpGet]
            public IActionResult GetComments(int productId)
            {
           
            var productComments = _commentService.GetComments(productId);
                return Json(productComments);
            }

            // POST: /Comment/AddComment/{productId}
            [HttpPost]
            public IActionResult AddComment(int productId, string text)
            {

            string _author = User.Identity.Name; // Assuming authentication is enabled
                
            _commentService.Create(productId ,_author, text);
                return Ok();
            }
        }


  
}
