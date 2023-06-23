using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModel.Comment;
using Market.Services.Interfaces;

using Market.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Market.DAL.Repositories;

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


        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            try
            {
                var product = await _commentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Comment not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _commentRepository.Delete(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteComment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
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
