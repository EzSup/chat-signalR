using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.DTOs
{
    /// <summary>
    /// A model with data required to create a new message
    /// </summary>
    public record MessageCreateDto(string userName, string chatName, string messageContent);
}
