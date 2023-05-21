using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Domain.ViewModel.Comment
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }

    }
}
