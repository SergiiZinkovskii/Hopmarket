using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModel.Comment;
using Market.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Interfaces
{
    public interface ICommentService
    {

        Task<IBaseResponse<Comment>> Create(int productId, string autor, string text);

        
        Task<BaseResponse<IEnumerable<CommentViewModel>>> GetComments(int productId);

  
    }
}
