public class OrderList
{
    private Node _root;
    private int _length;

    public int Length => _length;

    public void Add(Character character) 
    {
        if (_root == null) 
        {
            _root = new Node(character);
        }
    }

    public OrderList() 
    {
        _root = null;
    }
}

public class Node 
{
    private Character _next;
    private Character _previous;
    private Character _data;

    public Character Next => _next;
    public Character Previous => _previous;
    public Character Data => _data;

    public Node(Character c) 
    {
        _data = c;
    }
}
