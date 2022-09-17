using EasyMeets.Core.Common.DTO.User;
using EasyMeets.Core.DAL.Entities;
using EmailDto = EasyMeets.Core.Common.DTO.EmailDto;

namespace EasyMeets.Core.BLL.Interfaces;

public interface IEmailSenderService
{
    void Send(EmailDto email);
    EmailDto CreateInvitationsSubjectAndBody(UserDto currentUser, User userToInvite, Team team);
}