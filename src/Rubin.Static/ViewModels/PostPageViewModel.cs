using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubin.Static.Models;

namespace Rubin.Static.ViewModels
{
    public class PostPageViewModel
    {
        public PostPageViewModel(Post post)
        {
            this.Post = post;
        }

        public Post Post { get; set; }
    }
}
