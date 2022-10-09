using AutoMapper;
using message.dto;
using message.models;

namespace message.profile
{
    public class MessageProfile: Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>();

            CreateMap<CreateMessageDto, Message>();
        }
    }
}