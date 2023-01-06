using AutoMapper;
using MTSaglikAuthenticator.Business.Abstract;
using MTSaglikAuthenticator.Core.Utilities;
using MTSaglikAuthenticator.DataAccess.Abstract;
using MTSaglikAuthenticator.Entities.Models;
using MTSaglikAuthenticator.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTSaglikAuthenticator.Business.Concrete
{
    public class FileService : IFileService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FileService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public Result<FileViewModel> CreateFileForUser(FileViewModel model, string guidId)
        {
            if(model!=null)
            {
                var data = _mapper.Map<FileViewModel, File>(model);
                data.Id = Guid.NewGuid();
                data.CreatedBy = new Guid(guidId);
                data.CreatedDate = DateTime.Now;
                data.ModifiedBy = new Guid(guidId);
                data.ModifiedDate = DateTime.Now;
                _uow.file.Add(data);
                _uow.Save();

                return new Result<FileViewModel>(true, "Added is OK");
            }
            return new Result<FileViewModel>(false, "failed");
        }
    }
}
