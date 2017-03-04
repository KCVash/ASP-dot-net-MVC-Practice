namespace Isolate_Object_Dependencey
{
    public class Validation
    {
        public bool CheckAuthentication(string id, string password)
        {
            var accountDao = GetAccountDao();
            var passwordByDao = accountDao.GetPassword(id);

            var hash = GetHash();
            var hashResult = hash.GetHastResult(password);

            return passwordByDao == hashResult;
        }

        protected virtual Hash GetHash()
        {
            var hash = new Hash();
            return hash;
        }

        protected virtual AccountDao GetAccountDao()
        {
            var accountDao = new AccountDao();
            return accountDao;
        }
    }

    public class StubAccountDao : AccountDao
    {
        public override string GetPassword(string id)
        {
            return "KC";
        }
    }

    public class StubHash : Hash
    {
        public override string GetHastResult(string password)
        {
            return "KC";
        }
    }

    public class MyValidation : Validation
    {
        protected override AccountDao GetAccountDao()
        {
            return new StubAccountDao();
        }

        protected override Hash GetHash()
        {
            return new StubHash();
        }
    }
}
