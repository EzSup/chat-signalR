using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.DTOs
{
    public record MessageCreateDto(string userName, string chatName, string messageContent);
}
