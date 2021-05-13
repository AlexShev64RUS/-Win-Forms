using System;

public class Record : IComparable
{
        private String name;
        private int scopes;

        public Record(String name, int scopes)
        {
            this.name = name;
            this.scopes = scopes;
        }

        public String getName()
        {
            return name;
        }

        public int getScopes()
        {
            return scopes;
        }

        public int CompareTo(object obj)
        {
            Record r = (Record)obj;
            if (scopes < r.scopes)
                return 1;
            else if (scopes > r.scopes)
                return -1;
            else
                return 0;
        }
}
