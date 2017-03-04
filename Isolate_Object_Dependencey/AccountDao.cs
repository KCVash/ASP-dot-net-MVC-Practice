using System;

namespace Isolate_Object_Dependencey
{
    public class AccountDao
    {
        public AccountDao()
        {
        }

        public virtual string GetPassword(string id)
        {
            return id;
        }
    }
}