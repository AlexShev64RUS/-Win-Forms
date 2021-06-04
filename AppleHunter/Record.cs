using System;

namespace AppleHunter
{
    public class Record : IComparable
    {
        private readonly string _name;
        private readonly int _scopes;

        public Record(string name, int scopes)
        {
            _name = name;
            _scopes = scopes;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetScopes()
        {
            return _scopes;
        }

        public int CompareTo(object obj)
        {
            Record r = (Record) obj;
            if (_scopes < r._scopes)
                return 1;
            if (_scopes > r._scopes)
                return -1;
            return 0;
        }
    }
}