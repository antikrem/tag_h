using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Persistence.Query
{

    interface IQuery
    {
        void Execute();
    }


    class SaveImageQuery : IQuery
    {
        public void SetImage(HImage image)
        {

        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    class FetchAllImages : IQuery
    {

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }

    class FetchImage : IQuery
    {

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
