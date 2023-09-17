using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transofast.Domain.Entities.Concrete;
using Transofast.Domain.IRepositories;
using Transofast.Infrastructure.DataAccess;

namespace Transofast.Infrastructure.Repository
{
    public class MessageRepository:BaseRepository<Message>,IMessageRepository
    {
        public MessageRepository(TransofastDbContext transofastDb) : base(transofastDb) { }
    }
}
