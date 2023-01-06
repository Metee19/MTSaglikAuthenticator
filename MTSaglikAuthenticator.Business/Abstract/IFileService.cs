using MTSaglikAuthenticator.Core.Utilities;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Abstract
{
    public interface IFileService
    {
        Result<FileViewModel> CreateFileForUser(FileViewModel model, string guidId);
    }
}
