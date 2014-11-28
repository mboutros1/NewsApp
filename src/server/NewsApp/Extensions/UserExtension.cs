using System.Collections.Generic;
using AutoMapper;
using NewsApp.Model;
using NewsAppModel.Services;

namespace NewsAppModel.Extensions
{
    public static class UserExtension
    {
        static UserExtension()
        {
            Mapper.CreateMap<User, UserViewModel>().ReverseMap();
            Mapper.CreateMap<NewsFeed, NewsFeedDetailView>().ReverseMap();
            Mapper.CreateMap<Comment, CommentView>().ForMember(m => m.Id, h => h.MapFrom(m => m.CommentId)).ReverseMap();
        }

        public static UserViewModel ToViewModel(this User sender)
        {
            if (sender == null) return null;
            return Mapper.Map<User, UserViewModel>(sender);
        }

        public static NewsFeedDetailView ToDetailViewModel(this NewsFeed sender)
        {
            return Mapper.Map<NewsFeed, NewsFeedDetailView>(sender);
        }

        public static List<CommentView> ToViewModel(this IList<Comment> sender)
        {
            return Mapper.Map<IList<Comment>, List<CommentView>>(sender);
        }
    }
}