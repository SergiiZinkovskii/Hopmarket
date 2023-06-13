using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModel.Comment;
using Market.Services.Interfaces;

using Market.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Services
{
    public class CommentService : ICommentService

    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Comment> _commentRepository;

        public CommentService(IBaseRepository<User> userRepository, IBaseRepository<Comment> commentRepository)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IBaseResponse<Comment>> Create(int productId, string autor, string text)
        {
                 var comment = new Comment
                {
                    ProductId = productId,
                    Text = text,
                    Author = autor// Assuming authentication is enabled
                };
            await _commentRepository.Create(comment);
            return new BaseResponse<Comment>()
            {
                Description = "Відгук залишено",
                StatusCode = StatusCode.OK
            };
        }

        public async Task<BaseResponse<IEnumerable<CommentViewModel>>> GetComments(int productId)
        {
            var comments = await _commentRepository.GetAll()
                .Where(c => c.ProductId == productId)
                .ToListAsync();
            var commentViewModels = comments.Select(c => new CommentViewModel
            {
                Id = c.Id,
                ProductId = c.ProductId,
                Text = c.Text,
                Author = c.Author
            });
            return new BaseResponse<IEnumerable<CommentViewModel>>
            {
                Data = commentViewModels,
                Description = "Відгук додано",
                StatusCode = StatusCode.OK
            };
        }
    }
}
