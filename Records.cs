using System;

public class Records
{
	private String name;
	private int scopes;

	public Records (String name, int scopes) : IComparable
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

    public int compareTo(object o)
    {
        if (scopes > (Records)o.scopes)
            return 1;
        else if (scopes < (Records)o.scopes)
            return -1;
        else
            return 0;
    }
}
